using AutoMapper;
using $safeprojectname$.models.person;
using hexagonal.domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$.profiles
{
  public class PersonProfile : Profile
  {
    public PersonProfile()
    {
      CreateMap<PersonEntity, PersonRequestModel>().ReverseMap();
      CreateMap<PersonEntity, PersonResponseModel>().ReverseMap();
    }
  }
}
