using AutoMapper;
using hexagonal.application.models.genero;
using hexagonal.domain.entities;

namespace hexagonal.application.profiles
{
    public class GeneroProfile : Profile
    {
        public GeneroProfile()
        {
            CreateMap<GeneroEntity, GeneroRequestModel>().ReverseMap();
            CreateMap<GeneroEntity, GeneroResponseModel>().ReverseMap();
        }
    }
}