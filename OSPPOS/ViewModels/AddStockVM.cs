using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Models;
using System.ComponentModel.DataAnnotations;





namespace OSPPOS.ViewModels
{
    public class AddStockVM
    {
        // Select Lists
        public SelectList ProductList { get; set; }
        public SelectList SupplierList { get; set; }

        // Foreign Keys
        [Required(ErrorMessage = "Please select a product")]
        public string ProductId { get; set; }

        public string SupplierId { get; set; }

        // Stock Entry
        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Cost price cannot be negative")]
        [DataType(DataType.Currency)]
        public decimal? CostPrice { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        // Auto-set on save — not from the form
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
        public string AddedByUserId { get; set; }
    }
}