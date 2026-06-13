using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Models;
using System.ComponentModel.DataAnnotations;




namespace OSPPOS.ViewModels
{
    public class AddSupplierVM
    {
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = null!;
                public string Email {  get; set; } = string.Empty;
                public string Address { get; set; }= string.Empty;
                public bool IsActive { get; set; }=false;

    }
}




