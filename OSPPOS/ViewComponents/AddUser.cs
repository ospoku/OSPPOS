using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DMX.ViewModels;
using DMX.Data;
using DMX.Models;

namespace DMX.ViewComponents
{
    public class AddUser:ViewComponent
    {
        public readonly XContext prx;
        public readonly UserManager<AppUser> usm;
        public readonly RoleManager<AppRole> rmg;

        public AddUser(XContext PrintContext,UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            usm = userManager;
            prx = PrintContext;
            rmg = roleManager;
        }

        public IViewComponentResult Invoke()
        {
            AddUserVM addUserVM = new AddUserVM()
            {
               
                ApplicationRoles = rmg.Roles.Select(r => new SelectListItem { Text = r.Name, Value = r.Id }).ToList(),
            };
            return View(addUserVM);
        }

    }
}
