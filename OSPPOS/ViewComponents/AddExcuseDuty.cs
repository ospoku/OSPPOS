using DMX.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DMX.Models;

namespace DMX.ViewComponents
{
    public class AddExcuseDuty(UserManager<AppUser> userManager) : ViewComponent
    {
        public readonly UserManager<AppUser> usm = userManager;

        public IViewComponentResult Invoke()
        {
            AddExcuseDutyVM addExcuseDutyVM = new()
            {
                UsersList = new SelectList(usm.Users.ToList(), (nameof(AppUser.Id)),nameof(AppUser.Fullname))
            };
            return View(addExcuseDutyVM);
        }
    }
}
