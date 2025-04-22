using Application.Commands.Car;
using Domain.Interfaces;
using Domain.IRepostories;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static Domain.Models.Car;

namespace Infrastructure.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _context;

        public CarRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task CreateCarAsync(Car car)
        {
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
        }

        public async Task<Car?> FindByIdAsync(string carId)
        {
            return await _context.Cars.Include(c => c.Images).FirstOrDefaultAsync(x => x.Id == carId);
        }


        public async Task UpdateCarAsync(Car car)
        {
            _context.Cars.Update(car);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Car>> GetAllForSerller(string sellerId)
        {
            return await _context.Cars.Include(c => c.Images).Where(x => x.SellerId == sellerId && x.Status != CarStatus.Inactive).ToListAsync();
        }

        public async Task<List<Car>> GetAllForManager()
        {
            return await _context.Cars.Include(c => c.Images).Where(c => c.Status != CarStatus.Inactive).ToListAsync();
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<(List<Car> items, int totalCount)> SearchCarsAsync(SearchCarsCommand command ,bool isSeller, string id)
        {
            var query = _context.Cars.AsNoTracking().AsQueryable();

            if (isSeller)
            {
                query = query.Where(c => c.SellerId == id);
            }

            if (!string.IsNullOrEmpty(command.Brand))
                query = query.Where(c => c.Brand == command.Brand);

            if (!string.IsNullOrEmpty(command.Model))
                query = query.Where(c => c.Model == command.Model);

            if (!string.IsNullOrEmpty(command.Color))
                query = query.Where(c => c.Color == command.Color);

            if (command.Status.HasValue)
                query = query.Where(c => c.Status == command.Status.Value);
            else
            {
                // اگر کاربر هیچ فیلتر وضعیتی نداد، پس فروخته‌شده‌ها و غیرفعال‌ها رو حذف کن
                query = query.Where(c => c.Status != CarStatus.Sold && c.Status != CarStatus.Inactive);
            }
          
            if (command.MinPrice.HasValue)
                query = query.Where(c => c.Price >= command.MinPrice);

            if (command.MaxPrice.HasValue)
                query = query.Where(c => c.Price <= command.MaxPrice);

            if (command.MinYear.HasValue)
                query = query.Where(c => c.Year >= command.MinYear);

            if (command.MaxYear.HasValue)
                query = query.Where(c => c.Year <= command.MaxYear);

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((command.PageNumber - 1) * command.PageSize)
                .Take(command.PageSize)
                .ToListAsync();

            return (items, totalCount);

        }
    }
}
