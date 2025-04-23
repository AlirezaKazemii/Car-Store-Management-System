using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepostories
{
    public interface IOrderRepository
    {
        Task Add(Order order);
        Task<Order?> GetById(string orderId);
        Task SaveChange();
        Task<List<Order>> GetAllPendingOrders();
        Task<List<Order>> GetAllPaymentUploadedOrders();
        Task<List<Order>> GetOrdersByUserId(string userId);
    }
}
