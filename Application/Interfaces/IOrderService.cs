using Application.DTOs.Orders;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrder(string carId, ClaimsPrincipal user);
        Task ApproveOrders(string orderId);
        Task RejectOrders(string orderId);
        Task<List<GetAllPendingOrdersDto>> GetAllPendingOrders();
        Task UploadReceipt(string orderId, IFormFile receiptFile, string WebRootPath);
        Task<List<GetAllPaymentUploadedOrdersDto>> GetAllPaymentUploadedOrders();
        Task ApprovePayments(string orderId);   
        Task RejectPayments(string orderId);
        Task<InvoiceDto> GenerateInvoiceFromOrder(string orderId);
        Task<List<GetUserOrdersDto>> GetUserOrders(string userId);
    }
}
