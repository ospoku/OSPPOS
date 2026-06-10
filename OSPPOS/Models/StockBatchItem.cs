namespace OSPPOS.Models
{
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

