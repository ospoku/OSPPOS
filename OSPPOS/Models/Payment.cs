using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OSPPOS.Models
{
  

    public enum PaymentMethod { Cash, MobileMoney, BankTransfer, Cheque }

    public class Payment
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Payment.SaleOrderId))]
        public int SaleOrderId { get; set; }
        public SaleOrder SaleOrder { get; set; } = null!;

        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; } = PaymentMethod.Cash;
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public string? Reference { get; set; }   // MoMo transaction id, cheque number, etc.
        public string? Notes { get; set; }

        public string RecordedById { get; set; } = string.Empty;
        public AppUser? RecordedBy { get; set; }
    }


}
