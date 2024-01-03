using AutoMapper;
using hexagonal.application.models.log;
using hexagonal.domain.entities;

namespace hexagonal.application.profiles
{
    public class LogProfile : Profile
    {
        public LogProfile()
        {
            CreateMap<LogEntity, LogRequestModel>().ReverseMap();
        }
    }
}