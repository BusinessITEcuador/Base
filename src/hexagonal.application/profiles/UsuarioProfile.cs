using AutoMapper;
using hexagonal.application.models.usuario;
using hexagonal.domain.entities;

namespace hexagonal.application.profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<UsuarioEntity, UsuarioRequestModel>().ReverseMap();
            CreateMap<UsuarioEntity, UsuarioResponseModel>().ReverseMap();
        }
    }
}