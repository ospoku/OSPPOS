using OSPPOS.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OSPPOS.Models
{
  

    

    public class Payment :TableAudit
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Payment.SaleOrderId))]
        public int? SaleOrderId { get; set; }
        public SaleOrder? SaleOrder { get; set; } = null!;

        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public decimal Amount { get; set; }
        public int? PaymentMethodId {  get; set; }
        public PaymentMethod? PaymentMethod {  get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public string? Reference { get; set; }   // MoMo transaction id, cheque number, etc.
        public string? Notes { get; set; }

     
    }


}
