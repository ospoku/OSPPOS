using DMX.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DMX.ViewModels
{
    public class AddSubjectVM
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public string FacultyId { get; set; }
       
        public string DepartmentId { get; set; }
   
    }
}
                      
