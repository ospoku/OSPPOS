namespace OSPPOS.Models
{
   

    public enum PaymentStatus { Unpaid, Partial, Paid }
    public enum SaleType { Cash, Credit }

    public class SaleOrder
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;   // e.g. INV-2024-0001

        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public string? WalkInCustomerName { get; set; }           // for non-account sales

        public SaleType SaleType { get; set; } = SaleType.Cash;
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Unpaid;

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public DateTime? DueDate { get; set; }                    // for credit sales
        public string? Notes { get; set; }

        public string SoldById { get; set; } = string.Empty;
        public AppUser? SoldBy { get; set; }

        public decimal Discount { get; set; } = 0;               // flat discount
        public decimal DiscountPercent { get; set; } = 0;

        public ICollection<SaleOrderItem> Items { get; set; } = new List<SaleOrderItem>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();

        public decimal SubTotal => Items.Sum(i => i.LineTotal);
        public decimal DiscountAmount => DiscountPercent > 0
            ? SubTotal * DiscountPercent / 100 : Discount;
        public decimal TotalAmount => SubTotal - DiscountAmount;
        public decimal AmountPaid => Payments.Sum(p => p.Amount);
        public decimal AmountDue => TotalAmount - AmountPaid;

        public string CustomerDisplay => Customer?.Name ?? WalkInCustomerName ?? "Walk-in";
    }

    public class SaleOrderItem
    {
        public int Id { get; set; }
        public int SaleOrderId { get; set; }
        public SaleOrder SaleOrder { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal => Quantity * UnitPrice;
    }


}
}
