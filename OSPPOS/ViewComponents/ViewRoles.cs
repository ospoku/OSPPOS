using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DMX.Data;
using DMX.Models;
using DMX.ViewModels;

namespace DMX.ViewComponents
{
    public class ViewRoles( RoleManager<AppRole> roleManager) : ViewComponent
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
