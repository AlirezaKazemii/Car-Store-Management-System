using Application.Commands.Report;
using Application.Interfaces;
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
        //[Authorize]
        public async Task<IActionResult> GetDayliSalesReport([FromQuery] SalesReportCommand command)
        {
            var result = await _reportService.GenerateSalesReportByDateRange(command);

            if (result.Count == 0)
                return NotFound("No sales have been made on this date.");

            return Ok(result);
        }
    }
}
