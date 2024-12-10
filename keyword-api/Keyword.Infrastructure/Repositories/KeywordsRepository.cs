using Keyword.Infrastructure.Repositories._UnitOfWork;
using Keyword.Infrastructure.Repositories.Interfaces;

namespace Keyword.Infrastructure.Repositories
{
    public class KeywordsRepository : Repository, IKeywordsRepository
    {
        public KeywordsRepository() { }
        public KeywordsRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
