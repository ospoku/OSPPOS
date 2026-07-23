
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using OSPPOS.Models;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{
    public class ViewRoles( RoleManager<AppRole> roleManager) : Microsoft.AspNetCore.Mvc.ViewComponent
    {
       
        public readonly RoleManager<AppRole> rol=roleManager;

        public IViewComponentResult Invoke()
        { 
            var roles = rol.Roles.Where(r => r.IsDeleted == false).Select(r => new RolesVM
            {
                Name = r.Name,
               Description=r.Description,
                Id=r.Id,

            }).ToList();

            return View(roles);
        }
    }
}
