using System.ComponentModel.DataAnnotations;

namespace OSPPOS.ViewModels
{
    public class SaleItemVM
    {
        
            [Required] public int ProductId { get; set; }
            [Required, Range(1, 999999)] public int Quantity { get; set; }
            [Required, Range(0.01, 999999)] public decimal UnitPrice { get; set; }
            public string? ProductName { get; set; }
        }
    }

