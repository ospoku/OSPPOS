using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Enums;
using OSPPOS.Models;
using System.ComponentModel.DataAnnotations;



namespace OSPPOS.ViewModels
{
    public class AddInvoiceVM
    {
        public int CustomerId { get; set; }
       
  
        public DateTime? DueDate { get; set; }
        public string? Notes { get; set; }
       
        public decimal Discount { get; set; } = 0;
        public List<SaleItemVM> Items { get; set; } = [];

        public string? PaymentReference { get; set; }
        public int PaymentMethodId {  get; set; }
        public SelectList PaymentMethods { get; set; }
        public SelectList Customers { get; set; } = null!;
        public List<CreditInfoVM> CreditInfo { get; set; } = [];
        public List<Product> Products { get; set; } = [];
       
  
         

        public Enums.PaymentMethod PaymentMethod { get; set; }


    }
    }





