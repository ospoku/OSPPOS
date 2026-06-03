using DMX.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OSPPOS.Models;

namespace DMX.ViewComponents
{
    public class UserProfile(UserManager<AppUser>userManager):ViewComponent
    {
        public readonly UserManager<AppUser> usm = userManager;
        public IViewComponentResult Invoke()
        {

            




            return View();
        }
    }
}
