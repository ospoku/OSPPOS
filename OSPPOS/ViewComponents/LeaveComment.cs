using DMX.Data;
using DMX.Models;
using DMX.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DMX.ViewComponents
{
    public class LeaveComment(XContext dContext, UserManager<Models.AppUser> userManager) : ViewComponent
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

            Leave leaveToEdit = new();
            leaveToEdit = (from m in dcx.Leaves.Include(m => m.Comments.OrderBy(m => m.CreatedDate)) where m.LeaveId == Id select m).FirstOrDefault();

            LeaveCommentVM addCommentVM = new()
            {
               
                Comments = leaveToEdit.Comments,
                
                SelectedUsers = AssignedUsers,

        
                UsersList= new SelectList(usm.Users.ToList(), (nameof(AppUser.Id),nameof(AppUser.Fullname))),
            };
            

            return View(addCommentVM);
        }
    }
}
