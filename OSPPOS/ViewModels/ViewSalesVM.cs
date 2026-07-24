using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Models;
using System.ComponentModel.DataAnnotations;




namespace OSPPOS.ViewModels
{
    public class ViewSalesVM
    {
        public int? CustomerId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public string? WalkInCustomerName { get; set; }
        public string SaleType { get; set; }
        public DateTime? DueDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal AmountDue { get; set; }
        public string? Notes { get; set; }
        public decimal DiscountPercent { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
        public List<SaleItemVM> Items { get; set; } = [];

        // For initial cash payment
        public decimal CashReceived { get; set; } = 0;
        public PaymentMethod PaymentMethod { get; set; } 
        public string? PaymentReference { get; set; }
        public bool CanEdit { get;set; }
        public bool CanDelete { get;set; }
        = false;
        public bool CanInsert { get;set; } = false;
        public bool CanUpdate { get;set; }
    
        public bool CanPrint {  get; set; }
        public Guid PublicId { get; set; }
    }
}




