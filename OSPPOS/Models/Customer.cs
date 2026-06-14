using OSPPOS.Data;
using System.ComponentModel.DataAnnotations;

namespace OSPPOS.Models
{
 

    public class Customer:TableAudit
    {
        [Key]
        public int Id { get; set; }
        public Guid PublicId { get; set; }= Guid.NewGuid();
        public required string Name { get; set; } = string.Empty;
        [Required]
        public required string Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? TaxNumber { get; set; }

        public decimal CreditLimit { get; set; } = 0;
        public bool AllowCredit { get; set; } = false;
        public bool IsActive { get; set; } = true;
        

        public ICollection<SaleOrder> SaleOrders { get; set; } = new List<SaleOrder>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();

        // Computed
        public decimal TotalDebt => SaleOrders
            .Where(o => o.PaymentStatus != PaymentStatus.Paid)
            .Sum(o => o.AmountDue);
    }


}
