using System;

namespace Stepeco.Models
{
    public class RecommendationModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double? Minimum { get; set; }
        public double? Maximum { get; set; }
        public string Type { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
