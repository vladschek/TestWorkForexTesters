using AutoMapper;
using Common.DTOs;
using Common.DTOs;
using Core.Domain.Entities;


namespace Projects.API.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Project
            CreateMap<Project, ProjectResponseDTO>();
            CreateMap<ProjectCreateDTO, Project>();
            CreateMap<Indicator, IndicatorDTO>();
            CreateMap<IndicatorDTO, Indicator>();
            CreateMap<Chart, ChartDTO>();
            CreateMap<ChartDTO, Chart>();
            CreateMap<IndicatorUsage, IndicatorUsageDTO>().ReverseMap();

            //UserSettings
            CreateMap<UserSettings, UserSettingsDTO>().ReverseMap();
        }
    }
}
