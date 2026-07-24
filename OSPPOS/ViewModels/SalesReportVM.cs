using OSPPOS.Models;

namespace OSPPOS.ViewModels
{
  
        public class SalesReportVM
        {
            public DateTime From { get; set; } = DateTime.Today.AddDays(-30);
            public DateTime To { get; set; } = DateTime.Today;
            public List<SaleOrder> Orders { get; set; } = [];
            //public decimal TotalRevenue => Orders.Sum(o => o.TotalAmount);
            //public decimal TotalReceived => Orders.Sum(o => o.AmountPaid);
            //public decimal TotalOutstanding => Orders.Sum(o => o.AmountDue);
            public int TotalTransactions => Orders.Count;
        }

    }

