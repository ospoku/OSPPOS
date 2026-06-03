using DMX.Models;
using DMX.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static DMX.Constants.Permissions;

namespace DMX.ViewComponents
{
    public class EditProfile(UserManager<AppUser> userManager, RoleManager<AppRole> rolManager) : ViewComponent
    {
        public readonly UserManager<AppUser> usm = userManager;
        public readonly RoleManager<AppRole> rol = rolManager;



        public IViewComponentResult Invoke()
        {
            var userToEdit = usm.GetUserAsync(HttpContext.User).Result;

            EditProfileVM editUserVM = new();
            {
                editUserVM.Email = userToEdit.Email;
                editUserVM.Firstname = userToEdit.Firstname;
                editUserVM.Username = userToEdit.UserName;
                editUserVM.Lastname = userToEdit.Lastname;
                editUserVM.Telephone = userToEdit.PhoneNumber;



                switch (userToEdit.Picture)
                {
                    case null:
                        editUserVM.Picture = null;
                        break;
                    default:
                        editUserVM.Picture = Convert.ToBase64String(userToEdit.Picture);
                        
                        break;
                }
;


                return View(editUserVM);
            }
        }
    }
}
