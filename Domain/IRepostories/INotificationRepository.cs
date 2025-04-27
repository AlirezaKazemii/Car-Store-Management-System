using Domain.Models;

namespace Domain.IRepostories
{
    public interface INotificationRepository
    {
        Task Add(Notification notification);
        Task SaveChange();
        Task<List<Notification>> GetUnsentNotification();
        
    }
}
