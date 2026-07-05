using System.ComponentModel.DataAnnotations;

namespace OSPPOS.ViewModels
{
    public class StockBatchItemVM
    {
        [Required] public int ProductId { get; set; }
        [Required, Range(1, 999999)] public int Quantity { get; set; }
        [Required, Range(0.01, 999999)] public decimal UnitCost { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
