using OSPPOS.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OSPPOS.Models
{
    public class StockBatchItem:TableAudit
    {
            [Key]
            public int Id { get; set; }

       
     
        public int ProductId { get; set; }
          public Product Product { get; set; } = null!;
        
        //public StockBatch StockBatch { get; set; }
        public int Quantity { get; set; }
            public decimal UnitCost { get; set; }
            public decimal TotalCost => Quantity * UnitCost;

            // Optional: expiry tracking for drinks
            public DateTime? ExpiryDate { get; set; }
        }
    }

