using Application.Commands.Car;
using Application.DTOs.Car;
using Application.DTOs.Orders;
using Application.DTOs.User;
using AutoMapper;
using Domain.IRepostories;
using Domain.Models;

namespace Application.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, RegisterRequestDto>().ReverseMap();
            CreateMap<User, LoginRequestDto>().ReverseMap();

            CreateMap<Car, GetAllForSellerDto>()
                .ForMember(dest => dest.Images,
                 opt => opt.MapFrom(src => src.Images.Select(i => i.ImageUrl).ToList()));

            CreateMap<Car, GetAllForManagerDto>()
                .ForMember(dest => dest.Images,
                 opt => opt.MapFrom(src => src.Images.Select(i => i.ImageUrl).ToList()));

            CreateMap<Car, GetByIdCarDto>()
                .ForMember(dest => dest.Images,
                 opt => opt.MapFrom(src => src.Images.Select(i => i.ImageUrl).ToList()));

            CreateMap<SearchCarsCommand, CarSearchFilterCommand>().ReverseMap();
            CreateMap<Car, CarSearchFilterDto>().ReverseMap();
            CreateMap<Order, GetAllPendingOrdersDto>().ReverseMap();
            CreateMap<Order,GetAllPaymentUploadedOrdersDto>().ReverseMap();
        }
    }
}
