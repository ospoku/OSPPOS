using OSPPOS.Data;
using OSPPOS.Enums;
using System.ComponentModel.DataAnnotations;

namespace OSPPOS.Models
{
 

    public class Customer:TableAudit
    {
        [Key]
        public int CustomerId { get; set; }
        
        public  string Name { get; set; } = string.Empty;
        [Required]
        public string Phone { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? TaxNumber { get; set; }
        public decimal CreditLimit { get; set; } = 0;
        public bool AllowCredit { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public ICollection<SaleOrder> SaleOrders { get; set; } = [];
        public ICollection<Payment> Payments { get; set; } = [];

        // Computed
  
    }


}
