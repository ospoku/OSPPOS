using DMX.Data;

using DMX.Models;
using DMX.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace DMX.ViewComponents
{
    public class CommentExcuseDuty(XContext dContext, UserManager<AppUser> userManager , IDataProtectionProvider provider) : ViewComponent
    {
        public readonly XContext dcx = dContext;
        public readonly UserManager<AppUser> usm = userManager;
        public readonly IDataProtector protector=provider.CreateProtector("IdProtector");
        public IViewComponentResult Invoke(string Id)


        {
            var AssignedUsers= new List<string>();

            //var result = from u in dcx.Assignments where u.MemoId == Id select u.ApplicationUser.Id;
            //foreach (var user in result) {
            //    AssignedUsers.Add(user);
            //}

         
            var unprotectedId = protector.Unprotect( Id);
            ExcuseDuty dutyToComment = new();
            if(!Guid.TryParse(unprotectedId, out Guid dutyGuid))
            {
                return View("Error", "Invalid Excuse Duty Id format");
            }
            dutyToComment = (from m in dcx.ExcuseDuties.Include(m => m.ExcuseDutyComments.OrderBy(m => m.CreatedDate)).ThenInclude(u =>u.AppUser) where m.PublicId == dutyGuid select m).FirstOrDefault();

            ExcuseDutyCommentVM addCommentVM = new()
            {
               Diagnosis  = dutyToComment.Diagnosis,
                PatientName=dutyToComment.PatientName,
               PatientId  = dutyToComment.PatientId,
               Days=dutyToComment.ExcuseDays,
               DischargeDate=dutyToComment.DateofDischarge,
                SelectedUsers = AssignedUsers,
                CommentCount = dutyToComment.ExcuseDutyComments.Count(),
                Comments = dutyToComment.ExcuseDutyComments.OrderBy(m => m.CreatedDate).ToList(),

                UsersList = new SelectList(usm.Users.ToList(), (nameof(AppUser.Id),nameof(AppUser.Fullname))),
            };
            

            return View(addCommentVM);
        }
    }
}
