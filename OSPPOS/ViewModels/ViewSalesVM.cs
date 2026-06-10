using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Models;
using System.ComponentModel.DataAnnotations;




namespace OSPPOS.ViewModels
{
    public class ViewSalesVM
    {
        public int? CustomerId { get; set; }
        public string? WalkInCustomerName { get; set; }
        public SaleType SaleType { get; set; } = SaleType.Cash;
        public DateTime? DueDate { get; set; }
        public int TotalAmount { get; set; }
        public int AmountPaid { get; set; }
        public int AmountDue { get; set; }
        public string? Notes { get; set; }
        public decimal DiscountPercent { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
        public List<SaleItemVm> Items { get; set; } = [];

        // For initial cash payment
        public decimal CashReceived { get; set; } = 0;
        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Cash;
        public string? PaymentReference { get; set; }
        public bool CanEdit { get;set; }
        public bool CanDelete { get;set; }
        = false;
        public bool CanInsert { get;set; } = false;
        public bool CanUpdate { get;set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
            public int? PageCount { get; set; }
        public int? PageOffset { get; set; }
        public int? PageLimit { get; set; }
        public int? PageTotal { get; set; }
        public int? PageTotalCount { get;set; }
        public int? PageTotalOffset { get;set; }
        public int? PageTotalLimit { get;set; }
        public int? PageTotalTotalTotal { get;set; }
        public int? PageTotalTotalCount { get; set; }
        public bool CanPrint {  get; set; }
        public string PublicId { get; set; }
    }
}




