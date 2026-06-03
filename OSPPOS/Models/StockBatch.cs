using DMX.Models;

namespace OSPPOS.Models
{
   

    public class StockBatch
    {
        public int Id { get; set; }
        public string GRNNumber { get; set; } = string.Empty;   // e.g. GRN-2024-001
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; } = null!;

        public DateTime ReceivedDate { get; set; } = DateTime.UtcNow;
        public string? SupplierInvoiceRef { get; set; }
        public string? Notes { get; set; }
        public string ReceivedById { get; set; } = string.Empty;
        public AppUser? ReceivedBy { get; set; }

        public decimal TotalCost => Items.Sum(i => i.TotalCost);

        public ICollection<StockBatchItem> Items { get; set; } = new List<StockBatchItem>();
    }

    public class StockBatchItem
    {
        public int Id { get; set; }
        public int StockBatchId { get; set; }
        public StockBatch StockBatch { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public decimal TotalCost => Quantity * UnitCost;

        // Optional: expiry tracking for drinks
        public DateTime? ExpiryDate { get; set; }
    }


}
