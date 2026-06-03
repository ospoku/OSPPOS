using DMX.Data;
using DMX.Models;
using DMX.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DMX.ViewComponents
{
    public class ViewPerDiems(XContext context, UserManager<AppUser>userManager):ViewComponent
    {
        public readonly XContext ctx=context;
        public readonly UserManager<AppUser> usm = userManager;
        public IViewComponentResult Invoke()
        {


            // Get the list of staff members for the dropdown


            // Use navigation properties to join PerDiems, AppUser, and Department

            var perDiems = ctx.Users.Select(u=> new ViewPerDiemsVM 
            {
                Id=u.Id,
                Staff=u.Fullname, 
          Amount=ctx.PerDiems.Where(a=>a.UserId==u.Id).Select(a=>a.Amount).SingleOrDefault(),
            Department=u.DepartmentId,
            Rank=u.RankId
            }).ToList();


            // Pass the staff list to the view using ViewBag
        

            return View(perDiems);
        }
    }
}
