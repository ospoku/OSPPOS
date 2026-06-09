using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Models;
using System.ComponentModel.DataAnnotations;




namespace OSPPOS.ViewModels
{
    public class ViewProductsVM
    {
        public List<Product> Products { get; set; }
        public string Supplier { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public bool CanEdit {  get; set; }
        public bool CanDelete { get; set; } public bool CanInsert { get; set; }public bool CanUpdate { get; set; }
        public bool CanPrint { get; set; }  
        public bool CanSave { get; set; }
        public bool CanView { get; set; }
        public string PublicId { get; set; }
        public int TotalProducts { get; set; }
       
    
    
    }
}



