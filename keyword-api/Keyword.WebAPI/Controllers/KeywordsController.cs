using Keyword.Domain.Models;
using Keyword.Domain.Models.Keywords.SP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Keyword.WebAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = false)]

    public class KeywordsController : BaseController
    {
        public KeywordsController(IServiceProvider serviceProvider, IOptions<SectionConfiguration> configuration)
            : base(serviceProvider, configuration)
        {

        }

        private static readonly List<Keywords> keywords = new List<Keywords>
        {
            new Keywords { Name = "Elección presidencial de EE. UU. 2024", SearchVolume = 50000000 },
            new Keywords { Name = "Actualizaciones del conflicto en Ucrania", SearchVolume = 45000000 },
            new Keywords { Name = "Nominados al Óscar 2024", SearchVolume = 40000000 },
            new Keywords { Name = "Mejores smartphones de 2024", SearchVolume = 38000000 },
            new Keywords { Name = "Dieta basada en plantas", SearchVolume = 35000000 },
            new Keywords { Name = "Apoyo a la salud mental", SearchVolume = 30000000 },
            new Keywords { Name = "Canciones virales en TikTok", SearchVolume = 28000000 },
            new Keywords { Name = "Horario de los Juegos Olímpicos de Verano 2024", SearchVolume = 25000000 },
            new Keywords { Name = "Mejores autos eléctricos de 2024", SearchVolume = 22000000 },
            new Keywords { Name = "Clasificatorias de la Copa Mundial FIFA 2024", SearchVolume = 20000000 }

        };

        [HttpGet]
        [Route("GetKeywords")]
        [ProducesResponseType(200, Type = typeof(Keywords))]
        public ActionResult<IEnumerable<Keywords>> GetKeywords()
        {
            var sortedKeywords = keywords.OrderByDescending(k => k.SearchVolume).ToList();
            return Ok(sortedKeywords);
        }
    }

}
