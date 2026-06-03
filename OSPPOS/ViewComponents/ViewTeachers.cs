using Microsoft.AspNetCore.Mvc;
using DMX.Data;
using DMX.ViewModels;
using Microsoft.AspNetCore.Identity;
using DMX.Models;
namespace DMX.ViewComponents
{
    public class ViewTeachers(XContext dContext, UserManager<AppUser> userManager) : ViewComponent
    {
        public readonly XContext dcx = dContext;
        public readonly UserManager<AppUser> usm = userManager;
        public IViewComponentResult Invoke()
        {
            var user = usm.GetUserAsync(HttpContext.User).Result?.Id;
            var pettyList = dcx.PettyCashAssignments.Where(a => a.AppUser.Id == user || a.CreatedBy == user).Select(a => 
             new ViewPettyCashVM
            {
                PettyCashId = a.PublicId,
                Amount = a.PettyCash.Amount,

               
                
                Purpose = a.PettyCash.Purpose,
                ReferenceNumber=a.PettyCash.ReferenceNumber,
                CreatedDate = a.CreatedDate,
                Sender=a.CreatedBy
            }).OrderByDescending(a => a.CreatedDate).ToList();
            foreach (var cash in pettyList)
            {
                var sender = usm.FindByIdAsync(cash.Sender);
                   
               
            };
            return View(pettyList);
        }
    }
}
