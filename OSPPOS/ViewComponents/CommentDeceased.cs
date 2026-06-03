using DMX.Data;

using DMX.Models;
using DMX.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace DMX.ViewComponents
{
    public class CommentDeceased(XContext dContext, UserManager<AppUser> userManager) : ViewComponent
    {
        public readonly XContext dcx = dContext;
        public readonly UserManager<AppUser> usm = userManager;

        public IViewComponentResult Invoke(string Id)


        {
            var AssignedUsers= new List<string>();

            //var result = from u in dcx.Assignments where u.MemoId == Id select u.ApplicationUser.Id;
            //foreach (var user in result) {
            //    AssignedUsers.Add(user);
            //}

            var decodedId= HttpUtility.UrlDecode(Id)?.Replace(" ", "+");
            var unprotectedId = (decodedId);
            if(string.IsNullOrEmpty(unprotectedId))
            {
                return View("Error", "Invalid Deceased Id");
            }
            if(!Guid.TryParse(unprotectedId, out Guid deceasedGuid))
            {
                return View("Error", "Invalid Deceased Id format");
            }

            Deceased deceasedToEdit = (from m in dcx.Deceased.Include(m => m.DeceasedComments.OrderBy(m => m.CreatedDate)) where m.PublicId == deceasedGuid select m).FirstOrDefault();

            DeceasedCommentVM addCommentVM = new DeceasedCommentVM
            {
             
                Comments = deceasedToEdit.DeceasedComments,
         
                SelectedUsers = AssignedUsers,

        
                UsersList= new SelectList(usm.Users.ToList(), (nameof(AppUser.Id),nameof(AppUser.Fullname))),
            };
            

            return View(addCommentVM);
        }
    }
}
