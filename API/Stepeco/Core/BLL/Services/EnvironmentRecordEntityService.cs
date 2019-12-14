using Stepeco.Core.BLL.Base;
using Stepeco.Core.BLL.Interfaces;
using Stepeco.Core.DAL.Entities;
using Stepeco.Core.DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stepeco.Core.BLL.Services
{
    public class EnvironmentRecordEntityService : EntityService<EnvironmentRecord>, IEnvironmentRecordEntityService
    {
        public EnvironmentRecordEntityService(IEntityRepository<EnvironmentRecord> repository)
            : base(repository)
        {

        }
    }
}
