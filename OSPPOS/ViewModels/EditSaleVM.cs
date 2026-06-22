using OSPPOS.Models;

namespace OSPPOS.ViewModels
{
    public class EditSaleVM
    {
        public int? CustomerId { get; set; }
        public string? WalkInCustomerName { get; set; }
        public int SaleTypeId { get; set; } 
        public DateTime? DueDate { get; set; }
        public string? Notes { get; set; }
        public decimal DiscountPercent { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
        public List<SaleItemVm> Items { get; set; } = [];

        // For initial cash payment
        public decimal CashReceived { get; set; } = 0;
        public PaymentMethod PaymentMethod { get; set; } 
        public string? PaymentReference { get; set; }
    }
}




