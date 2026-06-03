using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DMX.ViewModels
{
    public class AddTeacherVM
    {
        [Required]
        public string Name { get; set; }

        public List<string> SelectedUsers { get; set; }
        [Required(ErrorMessage = "Please select assignees")]

        public SelectList UsersList { get; set; }
        [Required]
        public string Title { get; set; }
        public string DepartmentId { get; set; }
        public string FacultyId { get; set; }
    }
}
