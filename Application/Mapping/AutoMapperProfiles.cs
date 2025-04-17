using Application.DTOs;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User,RegisterRequestDto>().ReverseMap();
            CreateMap<User,LoginRequestDto>().ReverseMap();
        }
    }
}
