using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Models;
using System.ComponentModel.DataAnnotations;


namespace OSPPOS.ViewModels
{
    public class EditProductVM
    {
        public int ProductId { get; set; }
        public int? CategoryId { get; set; }
        public int SupplierId {  get; set; }
        public SelectList SupplierList { get; set; } = null!;
        public string? SKU { get; set; }
        public string? Name { get; set; } = null;
        public SelectList CategoryList { get; set; }= null!;
        public decimal WholesalePrice {  get; set; }
        public decimal SellingPrice { get; set; }
        public string? Description { get; set; }
        public decimal CurrentStock { get; set; } = 0;
        public decimal CostPrice { get; set; } = 0;
        public int ReorderLevel { get; set; }

        // For initial cash payment
        public SelectList UnitList { get; set; } = null!;
        public int UnitId { get; set; } 
        public bool IsActive { get; set; }
    }
}




