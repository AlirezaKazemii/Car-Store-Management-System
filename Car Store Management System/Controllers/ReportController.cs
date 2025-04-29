using Application.Commands.Report;
using Application.Interfaces;
using Application.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Car_Store_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }


        [HttpGet("Sales-daily")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetDailySalesReport([FromQuery] SalesReportCommand command)
        {
            var result = await _reportService.GenerateSalesReportByDateRange(command);

            if (result.Count == 0)
                return NotFound("No sales have been made on this date.");

            return Ok(new Result("", result));
        }



        [HttpGet("top-selling-car")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetTopSellingCars([FromQuery] GetTopSellingCarsCommand command)
        {
            var result = await _reportService.GetTopSellingCar(command);

            if (result.Count == 0)
                return NotFound("No sales have been made on this date.");

            return Ok(new Result("", result));
        }



        // گزارش سود و زیاد به دلیل اینکه در این سیستم مشتری و فروشنده هردو کاربر هستند بی مفهوم است

        //فقط در صورتی که سیستم، خودش "مالک خودروها" باشه.
        //اگر فروشگاه اتومبیل نقش یک "واسطه" رو بازی کنه (یعنی فقط بین فروشنده و خریدار ارتباط برقرار کنه و خودش صاحب خودرو نباشه)، مفهوم سود و زیان فرق می‌کنه یا حتی لازم نیست اصلاً محاسبه شه.


        [HttpGet("Customer-behavior")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> AnalyzeBehavior([FromQuery] string customerId)
        {
            var result = await _reportService.GetCustomerBehavior(customerId);

            return Ok(new Result("", result));
        }




    }
}
