using Stepeco.Core.BLL.Base;
using Stepeco.Core.BLL.Interfaces;
using Stepeco.Core.DAL.Entities;
using Stepeco.Core.DAL.Repository.Interface;

namespace Stepeco.Core.BLL.Services
{
    public class RecommendationEntityService : EntityService<Recommendation>, IRecommendationEntityService
    {
        public RecommendationEntityService(IEntityRepository<Recommendation> repository)
            :base(repository)
        {
            
        }
    }
}
