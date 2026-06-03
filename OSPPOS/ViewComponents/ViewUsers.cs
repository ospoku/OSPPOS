using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DMX.Data;
using DMX.Models;
using DMX.ViewModels;

namespace DMX.ViewComponents
{
    public class ViewUsers(XContext xContext, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager) : ViewComponent
    {
        public readonly XContext dcx = xContext;
        public readonly UserManager<AppUser> usm=userManager;
        public readonly RoleManager<AppRole> rol =roleManager;
        public IViewComponentResult Invoke()
        { 
            var userList = usm.Users.Where(u => u.IsDeleted == false).Select(u => new ViewUsersVM
            {
                UserId = u.Id,
                Fullname = u.Fullname,
              

                Username = u.UserName,
                Email=u.Email,
                Telephone=u.PhoneNumber,
              
               Role= string.Join(",", from p in dcx.UserRoles
                                       join role in rol.Roles on p.RoleId equals role.Id
                                       where p.UserId == u.Id
                                       select role.Name.ToString())
                

            }).ToList();

            return View(userList);
        }
    }
}
