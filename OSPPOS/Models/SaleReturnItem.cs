namespace OSPPOS.Models
{
    public class SaleReturnItem
    {
       
            public int Id { get; set; }

            public int SaleReturnId { get; set; }
            public SaleReturn SaleReturn { get; set; } = null!;

            public int ProductId { get; set; }

            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }

            public decimal LineTotal => Quantity * UnitPrice;
        }
    }

