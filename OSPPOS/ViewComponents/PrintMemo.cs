using DMX.Data;
using DMX.Models;
using DMX.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace DMX.ViewComponents
{
    public class PrintMemo(XContext dContext, UserManager<AppUser> userManager, IDataProtectionProvider provider) : ViewComponent
    {
        public readonly XContext dcx = dContext;
        public readonly UserManager<AppUser> usm = userManager;
        public readonly IDataProtector protector = provider.CreateProtector("IdProtector");
        public async  Task<IViewComponentResult> InvokeAsync(string Id)
        {
            
            var decryptedId=protector.Unprotect(Id);
            if (!Guid.TryParse(decryptedId, out Guid printGuid)) 
            {
            }

           
         var   memoToEdit = (from m in dcx.Memos.Include(c => c.MemoComments.OrderBy(c => c.CreatedDate)) where m.PublicId == printGuid select m).FirstOrDefault();

            MemoCommentVM addCommentVM = new()
            {
                MemoContent = memoToEdit.Content,
              Comments=memoToEdit.MemoComments,
                Title = memoToEdit.Title,
                Sender = (await usm.FindByIdAsync(memoToEdit.CreatedBy)).Fullname,
            
                
            };
            

            return View(addCommentVM);
        }
    }
}
