using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Orders
{
    public class UploadReceiptCommand
    {
        public string OrderId { get; set; }
        public IFormFile ReceiptFile { get; set; }
    }
}
