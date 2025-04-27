using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Report
{
    public class CustomerBehaviorDto
    {
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalSpent { get; set; }
        public DateTime? LastOrderDate { get; set; }
        public double? AveragePurchaseInterval { get; set; }
        public List<string> FavoriteBrand { get; set; }
        public bool IsActive { get; set; }
        public int LoyaltyScore { get; set; }
    }
}
