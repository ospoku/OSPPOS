using DocumentFormat.OpenXml.Drawing.Charts;

namespace OSPPOS.Models
{
    public class StockMovement
    {
       
            public int Id { get; set; }

            public int ProductId { get; set; }
            public Product Product { get; set; } = null!;

            public int Quantity { get; set; }  // negative for sale

            public string Type { get; set; } = "SALE"; // SALE, RESTOCK, ADJUSTMENT
        public int OrderId { get; set; }
            public SaleOrder Order { get; set; } // SaleOrderId

            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        }
    }
