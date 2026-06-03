using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMX.ViewModels
{
    public class AddUserVM
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string BranchId { get; set; }
        public SelectList Branches { get; set; }
        public List<SelectListItem> ApplicationRoles { get; set; }
        public string ApplicationRoleId { get; set; }
    }
}
