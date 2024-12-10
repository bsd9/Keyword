using Keyword.Application.Services;
using Keyword.Domain.Models;
using Keyword.Infrastructure.Context;
using Keyword.Utils.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
IEncryptionService Encrypt = new EncryptionService();
IConfigurationSection seccionConfiguracion = builder.Configuration.GetSection("SectionConfiguration");
IConfigurationSection seccionConnectionStrings = builder.Configuration.GetSection("ConnectionStrings");

builder.Services.Configure<SectionConfiguration>(seccionConfiguracion);
builder.Services.Configure<ConnectionStrings>(seccionConnectionStrings);
var configuracionAppSettings = seccionConfiguracion.Get<SectionConfiguration>();
var configuracionConnectionStrings = seccionConnectionStrings.Get<ConnectionStrings>();

string DecryptConnectionString(string encryptedConnectionString)
{
    return string.IsNullOrEmpty(encryptedConnectionString) ? null : Encrypt.Decrypt(encryptedConnectionString);
}

builder.Services.AddScoped<IEncryptionService, EncryptionService>();

#region Dynamic Services injection

var tempGeneralServices = typeof(_Service).Assembly.GetTypes()
                   .Where(w => !w.Name.StartsWith("_") &&
                   w.Name.EndsWith("Service"));

var serInterfaces = tempGeneralServices.Where(p => p.IsInterface);
var serImplementations = tempGeneralServices.Where(p => p.IsClass);

foreach (var serImplementation in serImplementations)
{
    var iterfaceName = $"I{serImplementation.Name}";
    var tempInterface = serInterfaces.FirstOrDefault(p => p.Name == iterfaceName);
    if (tempInterface != null)
    {
        builder.Services.AddScoped(tempInterface, serImplementation);
    }
}
#endregion

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidAudience = builder.Configuration["Authentication:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:SecretKey"]))
    };
});

builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("V1", new OpenApiInfo { Title = "Keyword Api", Version = "V1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        In = ParameterLocation.Header,
        Description = "Enter JWT with bearer format like 'Bearer [Token]'"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
    opt.CustomSchemaIds(type => type.FullName);
    opt.DocInclusionPredicate((docName, apiDesc) =>
    {
        return apiDesc.GroupName == null || !apiDesc.GroupName.Equals("Hidden", StringComparison.OrdinalIgnoreCase);
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowPolicySecureDomains", x =>
    {
        x.WithOrigins(configuracionAppSettings.SecureDomains)
         .AllowAnyHeader()
         .AllowAnyMethod()
         .AllowCredentials();
    });
});

builder.Services.AddControllers().AddJsonOptions(JSOptions =>
{
    JSOptions.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    JSOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsProduction())
{
    app.UseSwagger((opt) => opt.RouteTemplate = "swagger/{documentName}/swagger.json");
    app.UseSwaggerUI((opt) => {
        opt.SwaggerEndpoint("V1/swagger.json", "KeywordApi V1");
    });
}

app.UseCors("AllowPolicySecureDomains");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();