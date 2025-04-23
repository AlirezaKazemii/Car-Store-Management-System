using Application.Commands.Car;
using Application.DTOs.Car;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Domain.IRepostories;
using Domain.Models;
using System.Security.Claims;

namespace Application.CommandHandlers
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IImageStorageService _imageStorage;
        private readonly IMapper _mapper;

        public CarService(ICarRepository carRepository, IImageStorageService imageStorage, IMapper mapper)
        {
            _carRepository = carRepository;
            _imageStorage = imageStorage;
            _mapper = mapper;
        }

        public async Task<string> CreateCarAsync(CreateCarCommand command, string sellerId)
        {
            var car = new Car(command.Brand, command.Model, command.Year, command.Color, command.Price, command.VIN, command.Status, sellerId);

            if (command.Images != null)
            {
                var imagePath = await _imageStorage.SaveImageAsync(command.Images);

                foreach (var image in imagePath)
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
                throw new UnauthorizedAccessException("User must be logged in.");
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

        public async Task<bool> ChangeCarStatusAsync(ChangeCarStatusCommand command)
        {
            var car = await _carRepository.FindByIdAsync(command.CarId);

            if (car is null)
                return false;

            //  وقتی خودرو فروخته شده، باید قفل شه از تغییر وضعیت
            if (car.Status == Car.CarStatus.Sold)
                throw new InvalidOperationException("A sold car cannot change its status.");

            // فروش فقط از وضعیت "موجود" ممکن باشه
            if (command.NewStatus == Car.CarStatus.Sold && car.Status != Car.CarStatus.Available)
                throw new InvalidOperationException("Only available cars can be sold.");

            // برای غیرفعال کردن خودرو از اکشن متد حذف
            if (car.Status == Car.CarStatus.Inactive)
                throw new InvalidOperationException("Use the Delete operation to deactivate the vehicle.");

            // زمانی که خودرو درحال تعمیر باشه نباید قابل تغییر وضعیت به فروخته شده یا رزرو شده باشه
            if (car.Status == Car.CarStatus.Inmaintenance && command.NewStatus != Car.CarStatus.Available)
                throw new InvalidOperationException("First, the vehicle must change its status to Available.");


            car.ChangeStatus(command.NewStatus);

            await _carRepository.UpdateCarAsync(car);

            return true;
        }

        public async Task<PagedResultDto<CarSearchFilterDto>> SearchCarsAsync(CarSearchFilterCommand command , ClaimsPrincipal user)
        {

            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("User must be logged in.");
            }

            bool isSeller = user.IsInRole("Seller");

            var mappedToSearchCarsCommand = _mapper.Map<SearchCarsCommand>(command);
            var (listOfCars, totalCount) = await _carRepository.SearchCarsAsync(mappedToSearchCarsCommand , isSeller, userId);

            var CarsDto = _mapper.Map<List<CarSearchFilterDto>>(listOfCars);

            var pageResult = new PagedResultDto<CarSearchFilterDto>
            {
                Itmes = CarsDto,
                TotalCount = totalCount,
                PageNumber = command.PageNumber,
                PageSize = command.PageSize
            };

            return pageResult;
        }
    }
}
