namespace OSPPOS.Models
{
    public class SaleReturn
    {
      
            public int Id { get; set; }

            public int SaleOrderId { get; set; }
            public SaleOrder SaleOrder { get; set; } = null!;

            public ICollection<SaleReturnItem> Items { get; set; } = new List<SaleReturnItem>();

            public decimal TotalAmount => Items.Sum(i => i.LineTotal);

            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

            public string? Reason { get; set; }
        }
    }

