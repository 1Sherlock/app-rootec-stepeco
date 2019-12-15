using Stepeco.Core.BLL.Base;
using Stepeco.Core.BLL.Interfaces;
using Stepeco.Core.DAL.Entities;
using Stepeco.Core.DAL.Repository.Interface;

namespace Stepeco.Core.BLL.Services
{
    public class StepEntityService : EntityService<Step>, IStepEntityService
    {
        public StepEntityService(IEntityRepository<Step> repository)
            : base(repository)
        {

        }
    }
}
