using DMX.Data;
using DMX.Models;
using DMX.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace DMX.ViewComponents
{

    public class EditMemo(XContext dContext, UserManager<AppUser> userManager, IDataProtectionProvider provider) : ViewComponent
    {
        public readonly XContext dcx = dContext;
        public readonly UserManager<AppUser> usm = userManager;
        public readonly IDataProtector protector = provider.CreateProtector("IdProtector");
        public IViewComponentResult Invoke(string Id)

     
        {
           
            var decryptedId = protector.Unprotect(Id);
            if (!Guid.TryParse(decryptedId, out Guid memoGuid))
            {   return View("BadRequest", "Invalid memo ID format."); }
                Memo memoToEdit = new();
                memoToEdit = (from m in dcx.Memos where m.PublicId == memoGuid select m).FirstOrDefault();
                EditMemoVM editMemoVM = new()
                {
                    Title = memoToEdit.Title,
                    Content = memoToEdit.Content,
                    SelectedUsers = (from x in dcx.MemoAssignments where x.PublicId == memoGuid select x.UserId).ToList(),
                    UsersList = new SelectList(usm.Users.ToList(), (nameof(AppUser.Id), nameof(AppUser.Fullname))),
                };




                return View(editMemoVM);
            }


        }
    }



