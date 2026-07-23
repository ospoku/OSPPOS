
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OSPPOS.Models;

namespace OSPPOS.ViewComponents
{
    public class UserProfile(UserManager<AppUser>userManager): Microsoft.AspNetCore.Mvc.ViewComponent
    {
        public readonly UserManager<AppUser> usm = userManager;
        public IViewComponentResult Invoke()
        {

            




            return View();
        }
    }
}
