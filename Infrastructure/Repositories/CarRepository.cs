using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
