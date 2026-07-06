using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace OSPPOS.DTO
{
    public class AddProductDTO
    {    // Select Lists
        public SelectList CategoryList { get; set; }
        public SelectList SupplierList { get; set; }
        public SelectList UnitList { get; set; }

        // Foreign Keys
        [Required(ErrorMessage = "Please select a category")]
        public int CategoryId { get; set; }

        public int SupplierId { get; set; }
        public int UnitId { get; set; }

        // Product Details
        [Required(ErrorMessage = "Product name is required")]
        [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public string SKU { get; set; }

        // Pricing
        [Required(ErrorMessage = "Cost price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Cost price must be greater than 0")]
        [DataType(DataType.Currency)]
        public decimal CostPrice { get; set; }

        [Required(ErrorMessage = "Selling price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Selling price must be greater than 0")]
        [DataType(DataType.Currency)]
        public decimal SellingPrice { get; set; }

        [Range(0, double.MaxValue)]
        [DataType(DataType.Currency)]
        public decimal WholesalePrice { get; set; }

        // Stock
        [Range(0, int.MaxValue)]
        public int CurrentStock { get; set; } = 0;

        [Range(0, int.MaxValue)]
        public int ReorderLevel { get; set; } = 10;

        // Status
        public bool IsActive { get; set; } = true;

    }
}
