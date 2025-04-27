using Domain.IRepostories;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _context;

        public NotificationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
        }


        public async Task SaveChange()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<Notification>> GetUnsentNotification()
        {
            return await _context.Notifications.Where(n => !n.IsSent).ToListAsync();
        }

    }
}
