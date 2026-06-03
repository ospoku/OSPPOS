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
    public class CommentLetter(XContext dContext, UserManager<AppUser> userManager) : ViewComponent
    {
        public readonly XContext dcx = dContext;
        public readonly UserManager<AppUser> usm = userManager;
        
        public IViewComponentResult Invoke(string Id)
        {
            var decodedId = HttpUtility.UrlDecode(Id)?.Replace(" ", "+"); // sanitize
            var unprotectedId = (decodedId);
            if(!Guid.TryParse(unprotectedId, out Guid letterGuid))
            {
                return View("Error", "Invalid Letter Id format");
            }
            var letterToComment = (from d in dcx.Letters.Include(d=>d.LetterComments.OrderBy(l=>l.CreatedDate)) where d.PublicId ==letterGuid select d).FirstOrDefault();

            DocumentCommentVM addCommentVM = new()
            {
                MemoContent = letterToComment.AdditionalNotes,
                Comments = letterToComment.LetterComments.OrderBy(m => m.CreatedDate).ToList(),
                Title = letterToComment.ReferenceNumber,
                MemoId=letterToComment.PublicId,
                //SelectedUsers = AssignedUsers,
                Document=letterToComment.PDF,
                CommentCount = letterToComment.LetterComments.Count(),
                UsersList = new SelectList(usm.Users.ToList(), (nameof(AppUser.Id),nameof(AppUser.Fullname))),
            };
            

            return View(addCommentVM);
        }
    }
}
