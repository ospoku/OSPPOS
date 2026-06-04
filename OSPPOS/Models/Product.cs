using System.ComponentModel.DataAnnotations;

namespace OSPPOS.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public int? SupplierId { get; set; }
        public Supplier? Supplier { get; set; }

        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal WholesalePrice { get; set; }

        // Unit of measure e.g. "bottle", "crate", "carton"
        public string Unit { get; set; } = "bottle";
        public int UnitsPerCase { get; set; } = 1;

        public int ReorderLevel { get; set; } = 10;
        public int CurrentStock { get; set; } = 0;   // updated on each GRN / sale

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<StockBatchItem> StockBatchItems { get; set; } = new List<StockBatchItem>();
        public ICollection<SaleOrderItem> SaleOrderItems { get; set; } = new List<SaleOrderItem>();

        public bool IsLowStock => CurrentStock <= ReorderLevel;
        public decimal ProfitMargin => SellingPrice > 0
            ? Math.Round((SellingPrice - CostPrice) / SellingPrice * 100, 2) : 0;
    }


}
