using Stepeco.Core.DAL.Entities.Base;
using System;

namespace Stepeco.Core.DAL.Entities
{
    public class Step : Entity<int>
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
