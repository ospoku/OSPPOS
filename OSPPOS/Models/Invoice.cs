using OSPPOS.Data;
using System.ComponentModel.DataAnnotations;



    namespace OSPPOS.Models
    {
        public class Invoice : TableAudit
    {
        [Key]
            public int Id { get; set; }

            public string InvoiceNumber { get; set; } = string.Empty;

            public DateTime InvoiceDate { get; set; }
            public DateTime? DueDate { get; set; }

            // Relationships
            public ICollection<InvoiceItem> Items { get; set; } = [];
            public ICollection<Payment> Payments { get; set; } = [];

            // Discounts
            public decimal Discount { get; set; } = 0;
      

            // Core calculations
            public decimal SubTotal => Items.Sum(i => i.LineTotal);

  

            public decimal TotalAmount => SubTotal - Discount;

            public decimal AmountPaid => Payments.Sum(p => p.Amount);

            public decimal AmountDue => TotalAmount - AmountPaid;
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public string? WalkInCustomerName { get; set; }
        public string? Notes { get; set; }
    }



    
        public class InvoiceItem
        {
            [Key]
            public int Id { get; set; }

            // Foreign key → Invoice
            public int InvoiceId { get; set; }
            public Invoice Invoice { get; set; } = null!;

            // Product being sold
            public int ProductId { get; set; }
            public Product Product { get; set; } = null!;

            public string? Description { get; set; }

            public int Quantity { get; set; }

            public decimal UnitPrice { get; set; }

            // IMPORTANT: correct calculation
            public decimal LineTotal => Quantity * UnitPrice;
        }
    }
