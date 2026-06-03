using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DMX.ViewModels;
using DMX.Data;
using DMX.Models;

namespace DMX.ViewComponents
{
    public class AddRole:ViewComponent
    {
        public readonly XContext ctx;
        public readonly UserManager<AppUser> usm;
        public readonly RoleManager<AppRole> rmg;

        public AddRole(XContext xContext,UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            usm = userManager;
            ctx = xContext;
            rmg = roleManager;
        }

        public IViewComponentResult Invoke()
        {
            AddRoleVM addRoleVM = new()
            {


            };
            return View(addRoleVM);
        }

    }
}
