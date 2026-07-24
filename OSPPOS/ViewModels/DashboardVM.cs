using OSPPOS.Models;

namespace OSPPOS.ViewModels
{
    public class DashboardVM
    {
       
            public decimal TodaySales { get; set; }
            public decimal MonthSales { get; set; }
            public decimal TotalOutstanding { get; set; }
            public int LowStockCount { get; set; }
            public int TodayTransactions { get; set; }
    
        }
    }

