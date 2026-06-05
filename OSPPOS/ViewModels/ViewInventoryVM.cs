using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Models;
using System.ComponentModel.DataAnnotations;




namespace OSPPOS.ViewModels
{
    public class ViewInventoryVM
    {
        public List<Product> Products
        {
            get; set;
        }
        public int? CategoryId { get; set; }
        public string? Search { get; set; }
    }
}




