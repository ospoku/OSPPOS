using Microsoft.AspNetCore.Mvc;
using DMX.Data;
using DMX.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DMX.Models;

namespace DMX.ViewComponents
{
    public class ViewClassrooms(XContext dContext, UserManager<AppUser>userManager) : ViewComponent
    {
        public readonly XContext dcx = dContext;

        private readonly UserManager<AppUser> usm = userManager;

        public async Task<IViewComponentResult>InvokeAsync()
        {
            var user = (await usm.GetUserAsync(HttpContext.User)).Id;
            var memoList = dcx.MemoAssignments.Where(a=>a.AppUser.Id==user||a.Memo.CreatedBy==user & a.Memo.IsDeleted==false).Select(a => new ViewMemosVM
            {
                PublicId = a.Memo.PublicId,

                Content = a.Memo.Content,
                ReferenceNumber=a.Memo.ReferenceId,
             
                Assignees = (from u in usm.Users where u.Id == a.UserId select u.UserName).ToList(),
                Title = a.Memo.Title,
                
                CreatedDate = a.CreatedDate,
                CreatedBy=a.CreatedBy

            }).OrderByDescending(a=>a.CreatedDate).ToList();
            foreach (var memo in memoList)
            {
                var senderUser = await usm.FindByIdAsync(memo.CreatedBy);
                memo.Sender = senderUser?.Fullname;
            }
            return View(memoList);
        }
    }
}
