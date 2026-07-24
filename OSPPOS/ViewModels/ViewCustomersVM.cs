using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Enums;
using OSPPOS.Models;
using System.ComponentModel.DataAnnotations;




namespace OSPPOS.ViewModels
{
    public class ViewCustomersVM
    {
        public int? CustomerId { get; set; }


  
  

        public bool CanEdit {  get; set; }
        public bool CanDelete { get; set; }
        public bool CanInsert { get; set; } public bool CanUpdate { get; set; }
        public bool CanPrint {  get; set; }
   

        public  string Name { get; set; } = string.Empty;
        [Required]
        public  string Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? TaxNumber { get; set; }
        public decimal CreditLimit { get; set; } = 0;
        public bool AllowCredit { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public List<string> SaleOrders { get; set; }
     
        public ICollection<Payment> Payments { get; set; } = [];

        // Computed
        public decimal TotalDebt { get; set; }
        public Guid PublicId { get; set; }
    }


}





