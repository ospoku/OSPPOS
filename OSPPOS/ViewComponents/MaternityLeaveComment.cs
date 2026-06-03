using DMX.Data;
using DMX.Models;
using DMX.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DMX.ViewComponents
{
    public class MaternityLeaveComment(XContext dContext, UserManager<AppUser> userManager) : ViewComponent
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

            MaternityLeave maternityLeaveToEdit = new();
            maternityLeaveToEdit = (from m in dcx.MaternityLeaves.Include(m => m.Comments.OrderBy(m => m.CreatedDate)) where m.MaternityLeaveId == Id select m).FirstOrDefault();

            MaternityLeaveCommentVM addCommentVM = new()
            {
                Comments = maternityLeaveToEdit.Comments,
              
                SelectedUsers = AssignedUsers,

        
                UsersList= new SelectList(usm.Users.ToList(), (nameof(AppUser.Id),nameof(AppUser.Fullname))),
            };
            

            return View(addCommentVM);
        }
    }
}
