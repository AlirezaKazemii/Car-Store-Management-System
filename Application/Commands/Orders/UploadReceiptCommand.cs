using Microsoft.AspNetCore.Http;

namespace Application.Commands.Orders
{
    public class UploadReceiptCommand
    {
        public string OrderId { get; set; }
        public IFormFile ReceiptFile { get; set; }
    }
}
