using Keyword.Application.Services;
using Keyword.Application.Services.Interfaces;
using Keyword.Domain.Models;
using Keyword.Utils.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Keyword.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BaseController : Controller
    {
        protected IServiceProvider _serviceProvider;
        protected readonly SectionConfiguration _config;
        protected readonly IEncryptionService _encrytpionService;
        protected readonly ITokenService _tokenService;
        protected readonly IKeywordsService _keywordsService;

        public BaseController(IServiceProvider serviceProvider, IOptions<SectionConfiguration> configuration)
        {
            _config = configuration.Value;
            _serviceProvider = serviceProvider;
            _tokenService = serviceProvider.GetService(typeof(ITokenService)) as ITokenService;
            _keywordsService = serviceProvider.GetService(typeof(IKeywordsService)) as IKeywordsService;
            _encrytpionService = serviceProvider.GetService(typeof(IEncryptionService)) as IEncryptionService;
        }

    }
}
