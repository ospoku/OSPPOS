using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Models;
using System.ComponentModel.DataAnnotations;



namespace OSPPOS.ViewModels
{
    public class ViewProductVM
    {

        public List<Product> Products { get; set; }
    }
}



