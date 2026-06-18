using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Models;
using System.ComponentModel.DataAnnotations;



namespace OSPPOS.ViewModels
{
    public class AddSaleVM
    {
        public int? CustomerId { get; set; }
        public string? WalkInCustomerName { get; set; }
        public SelectList SaleTypes { get; set; }
        public int SaleTypeId { get; set; } 
        public DateTime? DueDate { get; set; }
        public string? Notes { get; set; }
        public decimal DiscountPercent { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
        public List<SaleItemVm> Items { get; set; } = [];

        // For initial cash payment
        public decimal CashReceived { get; set; } = 0;
        public int PaymentMethodId { get; set; } 
        public string? PaymentReference { get; set; }
        public int PaymentStatusId {  get; set; }
 

        // For dropdowns in the view
        public List<Customer> Customers { get; set; } = [];
        public List<Product> Products { get; set; } = [];
    }
}




