using Application.Commands.Car;
using Application.DTOs;
using Application.DTOs.Car;
using AutoMapper;
using Domain.IRepostories;
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

            CreateMap<Car,GetAllForSellerDto>()
                .ForMember(dest => dest.Images,
                 opt => opt.MapFrom(src => src.Images.Select(i => i.ImageUrl).ToList()));

            CreateMap<Car,GetAllForManagerDto>()
                .ForMember(dest => dest.Images,
                 opt => opt.MapFrom(src => src.Images.Select(i => i.ImageUrl).ToList()));

            CreateMap<Car,GetByIdCarDto>()
                .ForMember(dest => dest.Images,
                 opt => opt.MapFrom(src => src.Images.Select(i => i.ImageUrl).ToList()));

            CreateMap<SearchCarsCommand, CarSearchFilterCommand>().ReverseMap();
            CreateMap<Car, CarSearchFilterDto>().ReverseMap();
        }
    }
}
