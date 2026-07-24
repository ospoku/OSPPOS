using OSPPOS.Models;




namespace OSPPOS.ViewModels
{
    public class EditSupplierVM
    {
        public int? CustomerId { get; set; }
        public string? WalkInCustomerName { get; set; }
       
        public DateTime? DueDate { get; set; }
        public string? Notes { get; set; }
        public decimal DiscountPercent { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
        public List<SaleItemVM> Items { get; set; } = [];

        // For initial cash payment
        public decimal CashReceived { get; set; } = 0;
    
        public string? PaymentReference { get; set; }
    }
}




