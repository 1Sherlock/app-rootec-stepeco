using Stepeco.Core.DAL.Entities.Base;
using System;

namespace Stepeco.Core.DAL.Entities
{
    public class EnvironmentRecord : Entity<int>
    {
        public double LocationX { get; set; }
        public double LocationY { get; set; }
        public double? Temperature { get; set; }
        public double? Quality { get; set; }
        public double? Humidity { get; set; }
        public double? Pressure { get; set; }
        public double? NoiseLevel { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}