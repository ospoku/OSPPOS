using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Enums;
using OSPPOS.Models;
using System.ComponentModel.DataAnnotations;



namespace OSPPOS.ViewModels
{
    public class AddSaleVM
    {
        public int? CustomerId { get; set; }
        public string? WalkInCustomerName { get; set; }
    
       
        public DateTime? DueDate { get; set; }
        public string? Notes { get; set; }
        public decimal DiscountPercent { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
        public List<SaleItemVm> Items { get; set; } = [];

        public decimal CashReceived { get; set; } = 0;
        public int PaymentMethodId { get; set; } 
        public string? PaymentReference { get; set; }
        public int PaymentStatusId {  get; set; }
 public SelectList Customers { get; set; }
        public List<CreditInfoVM> CreditInfo { get; set; } = [];
        public List<Product> Products { get; set; } = [];
       
  
            public decimal InitialPayment { get; set; } = 0;

            public Enums.PaymentMethod PaymentMethod { get; set; } 

         
        }
    }





