using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Models;
using System.ComponentModel.DataAnnotations;




namespace OSPPOS.ViewModels
{
    public class AddSupplierVM
    {
        public int? CustomerId { get; set; }
        public string? WalkInCustomerName { get; set; }
        public SaleType SaleType { get; set; } = SaleType.Cash;
        public DateTime? DueDate { get; set; }
        public string? Notes { get; set; }
        public decimal DiscountPercent { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
        public List<SaleItemVm> Items { get; set; } = [];

        // For initial cash payment
        public decimal CashReceived { get; set; } = 0;
        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Cash;
        public string? PaymentReference { get; set; }
    }
}




