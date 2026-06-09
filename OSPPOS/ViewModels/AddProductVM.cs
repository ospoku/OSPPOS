using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Models;
using System.ComponentModel.DataAnnotations;




namespace OSPPOS.ViewModels
{
    public class AddProductVM
    {
   
        public List<string> SelectedCategory { get; set; }
        [Required(ErrorMessage = "Please select assignees")]
        public List<string> SelectedSupplier { get; set; }
        public List<string> SelectedUnit { get; set; }
        public SelectList CategoryList { get; set; }
        public SelectList SupplierList { get; set; }
        public SelectList UnitList { get; set; }
    }

}


