using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stepeco.Models
{
    public class EnvironmentRecordViewModel
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? Temperature { get; set; }
        public double? Quality { get; set; }
        public double? Humidity { get; set; }
        public double? Pressure { get; set; }
        public double? NoiseLevel { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
