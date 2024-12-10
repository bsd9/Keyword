using Keyword.Application.Services.Interfaces;
using Keyword.Domain.Models;
using Microsoft.Extensions.Options;

namespace Keyword.Application.Services
{
    public class KeywordsService : _Service, IKeywordsService
    {
        public KeywordsService(IOptions<ConnectionStrings> connectionStrings) : base("")
        {

        }
    }
}
