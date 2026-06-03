using DMX.Data;
using DMX.Models;
using DMX.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMX.ViewComponents
{
    public class AddPettyCash(UserManager<AppUser> userManager,XContext context) : ViewComponent
    {
        public readonly UserManager<AppUser> usm = userManager;
        public readonly XContext ctx= context;

        public IViewComponentResult Invoke()
        {
            AddPettyCashVM addPettyCashVM = new()
            {
                UsersList = new SelectList(usm.Users.ToList(), (nameof(AppUser.Id)),nameof(AppUser.Fullname)),
                Maximum=ctx.CashLimits.Select(p=>p.Amount).FirstOrDefault()
              
            };

            return View(addPettyCashVM);
        }
    }
}
