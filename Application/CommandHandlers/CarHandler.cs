using Application.Commands.Car;
using Application.DTOs.Car;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandHandlers
{
    public class CarHandler : ICarHandler
    {
        private readonly ICarRepository _carRepository;
        private readonly IImageStorageService _imageStorage;
        private readonly IMapper _mapper;

        public CarHandler(ICarRepository carRepository, IImageStorageService imageStorage, IMapper mapper)
        {
            _carRepository = carRepository;
            _imageStorage = imageStorage;
            _mapper = mapper;
        }

        public async Task<string> CreateCarAsync(CreateCarCommand command,string sellerId)
        {            
            var car = new Car(command.Brand, command.Model, command.Year, command.Color, command.Price, command.VIN, command.Status,sellerId);

            if(command.Images != null)
            {
                var imagePath = await _imageStorage.SaveImageAsync(command.Images);

                foreach(var image in imagePath)
                {
                    car.AddImage(image);
                }
            }

            await _carRepository.CreateCarAsync(car);

            return car.Id;
        }

        public async Task<Car> UpdateCarAsync(CarUpdateCommand command, string carId)
        {
            var car = await _carRepository.FindByIdAsync(carId);

            if (car is null)
                throw new Exception("The desired car was not found.");

            car.UpdateDetails(command.Brand, command.Model, command.Year, command.Color, command.Price, command.Status);

            await _carRepository.UpdateCarAsync(car);

            return car;
        }

        public async Task<List<GetAllForSellerDto>> GetAllForSeller(string sellerId)
        {
            var carList = await _carRepository.GetAllForSerller(sellerId);

            if (carList is null)
                throw new Exception("No user with this ID was found.");

            var carListDto = _mapper.Map<List<GetAllForSellerDto>>(carList);

            return carListDto;
        }

        public async Task<List<GetAllForManagerDto>> GetAllForManager()
        {
            var carList = await _carRepository.GetAllForManager();

            if (carList is null)
                throw new Exception("No user with this ID was found.");

            var carListDto = _mapper.Map<List<GetAllForManagerDto>>(carList);

            return carListDto;
        }

        public async Task<object?> GetByIdAsync(string carId, ClaimsPrincipal user)
        {

            var car = await _carRepository.FindByIdAsync(carId);

            if (car is null)
                return null;

            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("Seller must be logged in.");
            }

            var isManager = user.IsInRole("Manager");
            var isSeller = user.IsInRole("Seller");
            var isBuyer = user.IsInRole("Buyer");

            if (isBuyer && (car.Status == Car.CarStatus.Sold || car.Status == Car.CarStatus.Inactive || car.Status == Car.CarStatus.Reserved))
                return "Forbidden";

            if (isSeller && (car.SellerId != userId))
                return "Forbidden";

            var carDto = _mapper.Map<GetByIdCarDto>(car);

            return carDto;
        }

        public async Task DeleteAsync(string carId)
        {
            var car = await _carRepository.FindByIdAsync(carId);

            if (car is null)
                throw new Exception("User not found!");

            if (car.Status == Car.CarStatus.Inactive)
                throw new Exception("This car has already been Deleted.");

            car.ChangeStatus(Car.CarStatus.Inactive);

            await _carRepository.SaveChangeAsync();
        }
    }
}
