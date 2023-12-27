using AutoMapper;
using hexagonal.application.models.log;
using hexagonal.domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hexagonal.application.profiles
{
    public class LogProfile:Profile
    {
        public LogProfile()
        {
            CreateMap<LogEntity, LogRequestModel>().ReverseMap();
        }
    }
}
