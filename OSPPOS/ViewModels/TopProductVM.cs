using OSPPOS.Models;

namespace OSPPOS.ViewModels
{
    public class TopProductVM
    {
        public Product Product { get; set; } = null!;
        public int TotalQtySold { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalProfit { get; set; }
    }
}
