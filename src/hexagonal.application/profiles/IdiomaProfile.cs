using AutoMapper;
using hexagonal.application.models.idioma;
using hexagonal.domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hexagonal.application.profiles
{
    public class IdiomaProfile : Profile
    {
        public IdiomaProfile()
        {
            CreateMap<IdiomaEntity, IdiomaRequestModel>().ReverseMap();
            CreateMap<IdiomaEntity, IdiomaResponseModel>().ReverseMap();
        }
    }
}
