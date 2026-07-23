using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using OSPPOS.Models;
using OSPPOS.Data;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{
    public class AddRole(XContext xContext, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager) : ViewComponent
    {
        public readonly XContext ctx = xContext;
        public readonly UserManager<AppUser> usm = userManager;
        public readonly RoleManager<AppRole> rmg = roleManager;

        public IViewComponentResult Invoke()
        {
            AddRoleVM addRoleVM = new()
            {


            };
            return View(addRoleVM);
        }

    }
}
