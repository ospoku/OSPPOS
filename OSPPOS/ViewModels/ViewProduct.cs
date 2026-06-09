using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Models;
using System.ComponentModel.DataAnnotations;



namespace OSPPOS.ViewModels
{
    public class ViewProductVM
    {

        public List<Product> Products { get; set; }
        public bool CanEdit { get; set; }
        public bool CanPrint { get; set; }
    }
}



