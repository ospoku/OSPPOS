using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Enums;
using OSPPOS.Models;
using System.ComponentModel.DataAnnotations;

namespace OSPPOS.ViewModels
{
    public class ViewCustomerVM
    {
        public int CustomerId { get; set; }

        public  string Name { get; set; } = string.Empty;
        [Required]
        public required string Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? TaxNumber { get; set; }
        public decimal CreditLimit { get; set; } = 0;
        public bool AllowCredit { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public ICollection<SaleOrder> SaleOrders { get; set; } = [];
        public ICollection<Payment> Payments { get; set; } = [];
        public ICollection <Invoice> Invoices { get; set; } = [];
        // Computed
  
    }


}





