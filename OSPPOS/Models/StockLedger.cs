using OSPPOS.Data;

namespace OSPPOS.Models
{
    public class StockLedger : TableAudit
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public DateTime Date { get; set; }

        // Movement
        public int QuantityIn { get; set; }
        public int QuantityOut { get; set; }

        public int Balance { get; set; } // running balance

        // Reference tracking
        public string Reference { get; set; } = string.Empty; // e.g. INV-001, GRN-001
        public string TransactionType { get; set; } = string.Empty;

        public string? Notes { get; set; }
    }
}