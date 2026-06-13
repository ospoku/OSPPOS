using OSPPOS.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OSPPOS.Models
{
    public class Product:TableAudit
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public string? Description { get; set; }
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
 
        public Category Category { get; set; } = null!;
        [ForeignKey("SupplierId")]
        public int SupplierId { get; set; }

        public Supplier Supplier { get; set; } = null!;

        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal WholesalePrice { get; set; }

        // Unit of measure e.g. "bottle", "crate", "carton"
        [ForeignKey("UnitId")]
        public int UnitId { get; set; }
        public Unit Unit { get; set; } = null!;

        public int ReorderLevel { get; set; } = 10;
        public int CurrentStock { get; set; } = 0;   // updated on each GRN / sale

        public bool IsActive { get; set; } = true;
        

        public ICollection<StockBatchItem> StockBatchItems { get; set; } = [];
        public ICollection<SaleOrderItem> SaleOrderItems { get; set; } = [];

        public bool IsLowStock => CurrentStock <= ReorderLevel;
        public decimal ProfitMargin => SellingPrice > 0
            ? Math.Round((SellingPrice - CostPrice) / SellingPrice * 100, 2) : 0;
    }


}
