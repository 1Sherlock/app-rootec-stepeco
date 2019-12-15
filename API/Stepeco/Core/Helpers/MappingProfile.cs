using AutoMapper;
using Stepeco.Core.DAL.Entities;
using Stepeco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stepeco.Core.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Step, StepViewModel>();
            CreateMap<StepViewModel, Step>();
            CreateMap<StepPostModel, Step>();

            CreateMap<EnvironmentRecord, EnvironmentRecordViewModel>();
            CreateMap<EnvironmentRecordViewModel, EnvironmentRecord>();
            CreateMap<EnvironmentRecordPostModel, EnvironmentRecord>();

            CreateMap<Recommendation, RecommendationViewModel>();
            CreateMap<Recommendation, RecommendationModel>();
            CreateMap<RecommendationPostModel, Recommendation>();
            CreateMap<RecommendationEditModel, Recommendation>();
            CreateMap<RecommendationViewModel, Recommendation>();
            CreateMap<RecommendationModel, Recommendation>();
        }
    }
}
