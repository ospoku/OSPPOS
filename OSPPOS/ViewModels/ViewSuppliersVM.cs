using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Models;
using System.ComponentModel.DataAnnotations;




namespace OSPPOS.ViewModels
{
    public class ViewSuppliersVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ContactPerson { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; } = true;
 
        public ICollection<Product> Products { get; set; } = [];
        public ICollection<StockBatch> StockBatches { get; set; } = [];
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanInsert { get; set; } public bool CanUpdate { get; set; } 
        public bool CanPrint { get; set; }
        public string PublicId { get; set; }
    }
}




