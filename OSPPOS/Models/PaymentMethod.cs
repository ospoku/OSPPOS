using System.ComponentModel.DataAnnotations;

namespace OSPPOS.Models
{
    public class PaymentMethod
    {
        [Key]
        public int PaymentMethodId { get; set; } 
        public string Name { get; set; }=string.Empty;
        public string? Description { get; set; }
        public string Code { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
