using Application.Commands.Report;
using Application.DTOs.Report;

namespace Application.Interfaces
{
    public interface IReportService
    {
        Task<List<SalesReportDto>> GenerateSalesReportByDateRange(SalesReportCommand command);
        Task<List<GetTopSellingCarsDto>> GetTopSellingCar(GetTopSellingCarsCommand command);
        Task<CustomerBehaviorDto> GetCustomerBehavior(string customerId);
    }
}
