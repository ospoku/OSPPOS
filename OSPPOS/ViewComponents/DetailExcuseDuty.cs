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
    public class DetailExcuseDuty(XContext dContext, UserManager<AppUser> userManager, IDataProtectionProvider provider) : ViewComponent
    {
        public readonly XContext dcx = dContext;
        public readonly UserManager<AppUser> usm = userManager;
        public readonly IDataProtector protector = provider.CreateProtector("IdProtector");
        public IViewComponentResult Invoke(string Id)
        {
            
            var decryptedId = protector.Unprotect(Id);
            if (!Guid.TryParse(decryptedId, out Guid dutyGuid))
            {
                return View("Error", "Invalid Excuse Duty Id format");
            }

            ExcuseDuty excuseDutyDetail = new();
            excuseDutyDetail = (from a in dcx.ExcuseDuties where a.PublicId == dutyGuid & a.IsDeleted == false select a).FirstOrDefault();
            DetailExcuseDutyVM excuseDutyVM = new ()
            {
               
                DateofDischarge= excuseDutyDetail.DateofDischarge,
                ExcuseDays=new ExcuseDuty().ExcuseDays,
             
                Diagnosis = new ExcuseDuty().  Diagnosis,
               SelectedUsers = (from x in dcx.ExcuseDutyAssignments where x.PublicId == dutyGuid select x.UserId).ToList(),
                UsersList = new SelectList(usm.Users.ToList(), (nameof(AppUser.Id),nameof(AppUser.Fullname))),
            };
            return View(excuseDutyVM);
        }
    }
}
