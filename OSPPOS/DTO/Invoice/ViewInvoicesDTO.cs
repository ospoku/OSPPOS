using OSPPOS.Models;

namespace OSPPOS.DTO.Invoice
{
    public class ViewInvoicesDTO
    {
       public int CategoryId { get; set; }
        public string? Notes { get; set; } = string.Empty;
       public DateTime? DueDate { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
              public DateTime InvoiceDate { get; set; } 
        public decimal SellingPrice { get; set; }
                  public bool IsActive {  get; set; }
        public string SKU { get; set; } = string.Empty;
                public int SupplierId { get; set; }
               public int UnitId {  get; set; }
                 public decimal CostPrice {  get; set; }
            public int ReorderLevel { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal WholesalePrice { get; set; }


        // Relationships
        public ICollection<InvoiceItem> Items { get; set; } = [];
        public ICollection<Payment> Payments { get; set; } = [];

        // Discounts
        public decimal Discount { get; set; } = 0;
        public decimal DiscountPercent { get; set; } = 0;

        // Core calculations
        public decimal SubTotal => Items.Sum(i => i.LineTotal);

        public decimal DiscountAmount =>
            DiscountPercent > 0 ? SubTotal * DiscountPercent / 100 : Discount;

        public decimal TotalAmount => SubTotal - DiscountAmount;

        public decimal AmountPaid => Payments.Sum(p => p.Amount);

        public decimal AmountDue => TotalAmount - AmountPaid;
        public int? CustomerId { get; set; }
   

        public string? WalkInCustomerName { get; set; }
   
    }
}
