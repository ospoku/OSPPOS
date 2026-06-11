using OSPPOS.Data;
using System.ComponentModel.DataAnnotations;

namespace OSPPOS.Models
{
   

    public class StockBatch:TableAudit
    {
        [Key]
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

  


}
