using DMX.Data;

using DMX.Models;
using DMX.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Models;
using System.Web;

namespace OSPPOS.ViewComponents
{
    public class ServiceComment:ViewComponent
    {
        public readonly XContext dcx;
        public readonly UserManager<AppUser> usm;
        public readonly IDataProtector protector;
        public ServiceComment(XContext dContext, UserManager<AppUser> userManager,IDataProtectionProvider provider)
        {
            dcx = dContext;
            usm = userManager;  
            protector = provider.CreateProtector("IdProtector");
        }

        public IViewComponentResult Invoke(string Id)


        {
            var AssignedUsers= new List<string>();

            //var result = from u in dcx.Assignments where u.MemoId == Id select u.ApplicationUser.Id;
            //foreach (var user in result) {
            //    AssignedUsers.Add(user);
            //}
         
            var decryptedId=protector.Unprotect(Id);
            if(!Guid.TryParse(decryptedId, out Guid serviceGuid))
            {

            }

            ServiceRequest serviceRequestToEdit = new ServiceRequest();
            serviceRequestToEdit = (from m in dcx.ServiceRequests.Include(m => m.Comments.OrderBy(m => m.CreatedDate)) where m.PublicId == serviceGuid select m).FirstOrDefault();

            ServiceRequestCommentVM addCommentVM = new()
            {
                
                Comments = serviceRequestToEdit.Comments,
                Title = serviceRequestToEdit.RequestNumber,
                SelectedUsers = AssignedUsers,

        
                UsersList= new SelectList(usm.Users.ToList(), (nameof(AppUser.Id),nameof(AppUser.Fullname))),
            };
            

            return View(addCommentVM);
        }
    }
}
