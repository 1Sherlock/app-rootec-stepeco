using Stepeco.Core.DAL.Entities.Base;
using Stepeco.Core.Enums;
using System;

namespace Stepeco.Core.DAL.Entities
{
    public class Recommendation : Entity<int>
    {
        public string Description { get; set; }
        public double? Minimum { get; set; }
        public double? Maximum { get; set; }
        public RecordType Type { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
