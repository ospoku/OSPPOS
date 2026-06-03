using Microsoft.AspNetCore.Mvc;
using DMX.Data;
using DMX.ViewModels;
using static DMX.Constants.Permissions;
using Microsoft.AspNetCore.Identity;
using DMX.Models;

namespace DMX.ViewComponents
{
    public class ViewSickReports(XContext dContext,UserManager<AppUser>userManager) : ViewComponent
    {
        public readonly XContext dcx = dContext;
        public readonly UserManager<AppUser> usm = userManager;

        public IViewComponentResult Invoke()
        {
            var user = usm.GetUserAsync(HttpContext.User).Result?.UserName;
            var sickList = dcx.SickAssignments.Where(a => a.AppUser.UserName == user || a.Sick.CreatedBy == user).Select(a 
             => new ViewSickReportsVM
            {
                SickReportId=a.Sick.SickReportId,
                




   CreatedDate = a.Sick.CreatedDate,
            }).OrderByDescending(t => t.CreatedDate).ToList();
            return View(sickList);
        }
    }
}
