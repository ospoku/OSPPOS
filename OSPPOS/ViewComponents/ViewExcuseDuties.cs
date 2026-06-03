using Microsoft.AspNetCore.Mvc;
using DMX.Data;
using DMX.ViewModels;
using Microsoft.AspNetCore.Identity;
using DMX.Models;

namespace DMX.ViewComponents
{
    public class ViewExcuseDuties(XContext dContext, UserManager<AppUser> userManager) : ViewComponent
    {
        public readonly XContext dcx = dContext;
        public readonly UserManager<AppUser> usm = userManager;
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = (await usm.GetUserAsync(HttpContext.User)).Id;
            var iList = dcx.ExcuseDutyAssignments.Where(a => a.AppUser.Id == user || a.ExcuseDuty.CreatedBy == user & a.ExcuseDuty.IsDeleted == false).Select(a => new ViewExcuseDutiesVM
            {
                PublicId = a.ExcuseDuty.PublicId,
                DateofDischarge = a.ExcuseDuty.DateofDischarge,
                ExcuseDays = a.ExcuseDuty.ExcuseDays,
                OperationDiagnosis = a.ExcuseDuty. Diagnosis,
                CreatedDate = a.CreatedDate,
                CreatedBy = a.CreatedBy
            }).OrderByDescending(t => t.CreatedDate).ToList();
         foreach ( var i in iList ) 
            {
                var owner= await usm.FindByIdAsync(i.CreatedBy);
                i.Sender = owner?.Fullname;
            }

                return View(iList);
            }
        }
    }

