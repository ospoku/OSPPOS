using OSPPOS.Models;

namespace OSPPOS.ViewModels
{
    public class StockReportVM
    {
      
            public List<Product> LowStockProducts { get; set; } = [];
            public List<Product> AllProducts { get; set; } = [];
            public decimal TotalStockValue => AllProducts.Sum(p => p.CurrentStock * p.CostPrice);
            public decimal TotalRetailValue => AllProducts.Sum(p => p.CurrentStock * p.SellingPrice);
        }
    }

