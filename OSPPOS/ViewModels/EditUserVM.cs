using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DMX.ViewModels
{
    public class EditUserVM
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string BranchId { get; set; }
        public SelectList Branches { get; set; }
        public string ApplicationRoleId { get; set; }
        public List<SelectListItem> ApplicationRoles { get; set; }

            [Required]
            public string Id { get; set; }  // User ID (hidden in form)

      

          
            
        

            [Display(Name = "Email Confirmed")]
            public bool EmailConfirmed { get; set; }  // Checkbox for admin

          

            [Display(Name = "Active Account")]
            public bool IsActive { get; set; }  // Checkbox for Activate/Deactivate

       

            [Display(Name = "Reset Password")]
            public string ResetPassword { get; set; }  // Admin can set new password

        

            [Display(Name = "Selected Permissions")]
            public List<string> SelectedPermissions { get; set; } = new List<string>(); // For permission assignment
        }
    }




