using OSPPOS.Data;
using OSPPOS.Enums;
using OSPPOS.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OSPPOS.Models
{

    public class SaleOrder : TableAudit
    {
        public int Id { get; set; }

        public string OrderNumber { get; set; } = string.Empty;

        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public DateTime? DueDate { get; set; }   // optional (only if you care about deadlines)

        public string? Notes { get; set; }

        public ICollection<SaleOrderItem> Items { get; set; } = [];
        
     
    }
}

public class SaleOrderItem
{
    [Key]
    public int Id { get; set; }
    [ForeignKey(nameof(SaleOrderItem.SaleOrderId))]
    public int SaleOrderId { get; set; }
    public SaleOrder SaleOrder { get; set; } = null!;
    [ForeignKey(nameof(SaleOrderItem.ProductId))]
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal LineTotal => Quantity;
}
