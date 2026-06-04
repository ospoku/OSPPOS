using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using OSPPOS.Models;
using OSPPOS.ViewModels;


namespace DMX.ViewComponents
{
    public class EditUser(UserManager<AppUser> userManager, RoleManager<AppRole> rolManager, IDataProtectionProvider provider) : ViewComponent
    {
        public readonly UserManager<AppUser> usm = userManager;
        public readonly RoleManager<AppRole> rol = rolManager;
        public readonly IDataProtector protector = provider.CreateProtector("IdProtector");

        public IViewComponentResult Invoke(string Id)
        {

            AppUser userToEdit = (from u in usm.Users where u.Id == @protector.Unprotect(Id) select u).FirstOrDefault();

            EditUserVM editUserVM = new()
            {
              
                Email = userToEdit.Email,
                Firstname = userToEdit.Firstname,
                Username = userToEdit.UserName,
                Lastname = userToEdit.Lastname,
                PhoneNumber = userToEdit.PhoneNumber,
            };

            return View(editUserVM);
        }
    }
}
