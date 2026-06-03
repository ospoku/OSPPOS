using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DMX.Data;
using DMX.Models;
using DMX.ViewModels;


namespace DMX.ViewComponents
{
    public class OtherDetails(UserManager<AppUser> userManager, RoleManager<AppRole> rolManager) : ViewComponent
    {
        public readonly UserManager<AppUser> usm = userManager;
        public readonly RoleManager<AppRole> rol = rolManager;
        

        public IViewComponentResult Invoke()
        {

            AppUser userToEdit = usm.GetUserAsync(HttpContext.User).Result;

            EditProfileVM editUserVM = new()
            {
              
                Email = userToEdit.Email,
                Firstname = userToEdit.Firstname,
                Username = userToEdit.UserName,
                Lastname = userToEdit.Lastname,
                Telephone = userToEdit.PhoneNumber,
            };

            return View(editUserVM);
        }
    }
}
