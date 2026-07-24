using System.ComponentModel.DataAnnotations;

namespace OSPPOS.Models
{
    public class CreditSettings
    {
        [Key]
        public int CreditId { get; set; }
        public Customer Customer { get; set; }
        public bool IsAllowed {  get; set; }
        public decimal Amount { get; set; }
    }
}
