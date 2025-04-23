using Application.Commands.Orders;
using Application.Interfaces;
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
            await _orderService.CreateOrder(command.CarId, User);

            return Ok("Order successfully placed.");
        }


        [HttpPost("Approve-Orders")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> ApproveOrders(string orderId)
        {
            await _orderService.ApproveOrders(orderId);

            return Ok("Order confirmed");
        }


        [HttpPost("Reject-Orders")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> RejectOrders(string orderId)
        {
            await _orderService.RejectOrders(orderId);

            return Ok("The order was rejected.");
        }


        [HttpGet("GetAll-Pending-OrdersList")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetAllPendingOrders()
        {
            return Ok(await _orderService.GetAllPendingOrders());
        }


        [HttpPost("Upload-Receipt")]
        [Authorize(Roles = "Buyer")]
        public async Task<IActionResult> UploadReceipt([FromForm] UploadReceiptCommand command)
        {
            await _orderService.UploadReceipt(command.OrderId, command.ReceiptFile, _env.WebRootPath);

            return Ok("The receipt was successfully uploaded.");
        }


        [HttpGet("GetAll-PaymentUploaded-OrdersList")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetAllPaymentUploadedOrders()
        {
            return Ok(await _orderService.GetAllPaymentUploadedOrders());
        }


        [HttpPost("Approve-Payments")]
        //[Authorize(Roles = "Manager")]
        public async Task<IActionResult> ApprovePayments(ApprovePaymentsCommand command)
        {
            await _orderService.ApprovePayments(command.OrderId);

            return Ok("Payment confirmed.");
        }


        [HttpPost("Reject-Payments")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> RejectPayments(RejectPaymentsCommand command)
        {
            await _orderService.RejectPayments(command.orderId);

            return Ok("The order was rejected.");
        }

    }
}
