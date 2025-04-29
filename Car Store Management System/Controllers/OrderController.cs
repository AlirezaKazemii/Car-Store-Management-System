using Application.Commands.Orders;
using Application.Interfaces;
using Application.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Car_Store_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IWebHostEnvironment _env;

        public OrderController(IOrderService orderService, IWebHostEnvironment env)
        {
            _orderService = orderService;
            _env = env;
        }


        [HttpPost("CreateOrder")]
        [Authorize(Roles = "Buyer")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            await _orderService.CreateOrder(command, User);

            return Ok(new Result("Order successfully placed."));
        }


        [HttpPost("Approve-Orders")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> ApproveOrders(string orderId)
        {
            await _orderService.ApproveOrders(orderId);

            return Ok(new Result("Order confirmed"));
        }


        [HttpPost("Reject-Orders")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> RejectOrders(string orderId)
        {
            await _orderService.RejectOrders(orderId);

            return Ok(new Result("The order was rejected."));
        }


        [HttpGet("GetAll-Pending-OrdersList")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetAllPendingOrders()
        {
            var result = await _orderService.GetAllPendingOrders();
            return Ok(new Result("",result));
        }


        [HttpPost("Upload-Receipt")]
        [Authorize(Roles = "Buyer")]
        public async Task<IActionResult> UploadReceipt([FromForm] UploadReceiptCommand command)
        {
            await _orderService.UploadReceipt(command.OrderId, command.ReceiptFile, _env.WebRootPath);

            return Ok(new Result("The receipt was successfully uploaded."));
        }


        [HttpGet("GetAll-PaymentUploaded-OrdersList")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetAllPaymentUploadedOrders()
        {
            var result = await _orderService.GetAllPaymentUploadedOrders();
            return Ok(new Result("", result));
        }


        [HttpPost("Approve-Payments")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> ApprovePayments(ApprovePaymentsCommand command)
        {
            await _orderService.ApprovePayments(command);

            return Ok(new Result("Payment confirmed."));
        }


        [HttpPost("Reject-Payments")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> RejectPayments(RejectPaymentsCommand command)
        {
            await _orderService.RejectPayments(command.orderId);

            return Ok(new Result("The order was rejected."));
        }

        [HttpGet("Generate-Invoice-FromOrder")]
        [Authorize(Roles = "buyer")]
        public async Task<IActionResult> GenerateInvoiceFromOrder([FromQuery] GenerateInvoiceFromOrderCommand command)
        {
            var Result = await _orderService.GenerateInvoiceFromOrder(command.OrderId);

            return Ok(new Result("",Result));
        }


        [HttpGet("Get-user_orders")]
        [Authorize(Roles = "Buyer")]
        public async Task<IActionResult> GetUserOrders([FromQuery] GetUserOrdersCommand command)
        {
            var orderResult = await _orderService.GetUserOrders(command.UserId);

            return Ok(new Result("",orderResult));
        }
    }
}
