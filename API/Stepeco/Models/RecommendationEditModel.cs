using Stepeco.Core.Enums;
using System;

namespace Stepeco.Models
{
    public class RecommendationEditModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double? Minimum { get; set; }
        public double? Maximum { get; set; }
        public RecordType Type { get; set; }
        public string Keyword { get; set; }
    }
}
