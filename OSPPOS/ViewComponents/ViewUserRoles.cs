
using Microsoft.AspNetCore.Mvc;
using DMX.Data;
using DMX.Models;
using DMX.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace DMX.ViewComponents
{
    public class ViewUserRoles(XContext context, UserManager<AppUser> userManager) : ViewComponent
    {
        public readonly XContext dcx = context;
        public readonly UserManager<AppUser> usm=userManager;

        public IViewComponentResult Invoke()
        {
            var userList = usm.Users.Select(u => new ViewUserRolesVM
            {
                UserId = u.Id,
                Name = u.Fullname,
                Roles = (from x in dcx.UserRoles join r in dcx.Roles on x.RoleId equals r.Id where x.UserId == u.Id select r.Name).ToList(),

            }).ToList();

            return View(userList);
        }
    }
}
