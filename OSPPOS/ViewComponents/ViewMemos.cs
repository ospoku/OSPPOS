using DMX.Data;
using DMX.Models;
using DMX.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static DMX.Constants.Permissions;


namespace DMX.ViewComponents
{
    public class ViewMemos(
        XContext context,
        UserManager<AppUser> userManager,
        IAuthorizationService authorization) : ViewComponent
    {
        private readonly XContext ctx = context;
        private readonly UserManager<AppUser> usm = userManager;
        private readonly IAuthorizationService auth = authorization;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentUser = await usm.GetUserAsync(HttpContext.User);

            var memoAssignments = await ctx.MemoAssignments
                .Include(a => a.Memo)
                .Include(a => a.AppUser)
                .Where(a =>
                    a.AppUser.Id == currentUser.Id ||
                    (a.Memo.CreatedBy == currentUser.Id && !a.Memo.IsDeleted))
                .OrderByDescending(a => a.CreatedDate)
                .ToListAsync();

            var viewModel = new List<ViewMemosVM>();

            foreach (var assignment in memoAssignments)
            {
                var authorizationResult = await auth.AuthorizeAsync(
                    HttpContext.User,
                    assignment.Memo,
                    "MemoOwnerPolicy");
                var canPrint = HttpContext.User.HasClaim("Permission", "Permission.Print.Memo");
                var sender = await usm.FindByIdAsync(assignment.Memo.CreatedBy);

                viewModel.Add(new ViewMemosVM
                {
                    PublicId = assignment.Memo.PublicId,
                    Title = assignment.Memo.Title,
                    Content = assignment.Memo.Content,
                    ReferenceNumber = assignment.Memo.ReferenceId,
                    CreatedDate = assignment.CreatedDate,
                    CreatedBy = assignment.Memo.CreatedBy,
                    Sender = sender?.Fullname,
                    Assignees = memoAssignments
                        .Where(x => x.MemoId == assignment.MemoId)
                        .Select(x => x.AppUser.UserName)
                        .Distinct()
                        .ToList(),
                    CanEdit = authorizationResult.Succeeded,
                    CanPrint=canPrint
                    
                });
            }

            return View(viewModel);
        }
    }
}
