using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace OSPPOS.ViewModels
{
    public class AddStockBatchVM
    {
        // Select Lists
        public SelectList? SupplierList { get; set; }
        public SelectList ProductList { get; set; }

        // StockBatch fields
        public string GRNNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a supplier")]
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Received date is required")]
        public DateTime ReceivedDate { get; set; } = DateTime.UtcNow;

        public string? SupplierInvoiceRef { get; set; }
        public string? Notes { get; set; }

        // Line items
        [Required(ErrorMessage = "Please add at least one product")]
        public List<AddStockBatchItemVM> Items { get; set; } = [];
    }

    public class AddStockBatchItemVM
    {
        [Required(ErrorMessage = "Please select a product")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Unit cost is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit cost must be greater than 0")]
        public decimal UnitCost { get; set; }

        public DateTime? ExpiryDate { get; set; }

        // Computed display only — not posted
        public decimal TotalCost => Quantity * UnitCost;
    }
}
