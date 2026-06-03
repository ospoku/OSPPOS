namespace OSPPOS.Models
{
 

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? TaxNumber { get; set; }

        public decimal CreditLimit { get; set; } = 0;
        public bool AllowCredit { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<SaleOrder> SaleOrders { get; set; } = new List<SaleOrder>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();

        // Computed
        public decimal TotalDebt => SaleOrders
            .Where(o => o.PaymentStatus != PaymentStatus.Paid)
            .Sum(o => o.AmountDue);
    }


}
