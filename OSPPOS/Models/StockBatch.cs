using OSPPOS.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OSPPOS.Models
{
   

    public class StockBatch:TableAudit
    {
        [Key]
        public int Id { get; set; }
        public string GRNNumber { get; set; } = string.Empty;   // e.g. GRN-2024-001
        
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; } = null!;
        //[ForeignKey(nameof(ProductId))]
        //public int ProductId { get; set; }
        //public Product Product { get; set; } = null!;
        public DateTime ReceivedDate { get; set; } = DateTime.UtcNow;
        public string? SupplierInvoiceRef { get; set; }
        public string? Notes { get; set; }
    

        public decimal TotalCost => Items.Sum(i => i.TotalCost);

        public ICollection<StockBatchItem> Items { get; set; } = [];
    }

  


}
