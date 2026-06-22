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

        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; } = null!;
        public string? WalkInCustomerName { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public DateTime? DueDate { get; set; }   // optional (only if you care about deadlines)

        public string? Notes { get; set; }

        public decimal Discount { get; set; } = 0;
        public decimal DiscountPercent { get; set; } = 0;

        public ICollection<SaleOrderItem> Items { get; set; } = [];
        public ICollection<Payment> Payments { get; set; } = [];

        // 🔥 COMPUTED VALUES (single source of truth)
        public decimal SubTotal => Items.Sum(i => i.LineTotal);

        public decimal DiscountAmount =>
            DiscountPercent > 0 ? SubTotal * DiscountPercent / 100 : Discount;

        public decimal TotalAmount => SubTotal - DiscountAmount;

        public decimal AmountPaid => Payments.Sum(p => p.Amount);

        public decimal AmountDue => TotalAmount - AmountPaid;

        public PaymentState PaymentState =>
            AmountPaid >= TotalAmount ? PaymentState.Paid :
            AmountPaid > 0 ? PaymentState.Partial :
            PaymentState.Unpaid;

        public string CustomerDisplay =>
            Customer?.Name ?? WalkInCustomerName ?? "Walk-in";
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
