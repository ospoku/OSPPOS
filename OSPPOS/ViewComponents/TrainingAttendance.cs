using DMX.Data;

using DMX.Models;
using DMX.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared;

namespace DMX.ViewComponents
{
    public class TrainingAttendance(UserManager<AppUser> userManager, XContext xContext, IDataProtectionProvider provider) : ViewComponent
    {
        public readonly UserManager<AppUser> usm = userManager;
        public readonly XContext dcx= xContext; public readonly IDataProtector protector = provider.CreateProtector("IdProtector");
        public IViewComponentResult Invoke(string Id)
        {


            TrainingAttendanceVM addAttendanceVM = new() 
            {
                EventName= dcx.Trainings.Where(x=>x.TrainingId==protector.Unprotect(Id)).Select(x=>x.EventName).Single(),

                Attendees = new SelectList(usm.Users.ToList(), "Id", "Fullname"),

            };
            return View(addAttendanceVM);
        }
    }
}
