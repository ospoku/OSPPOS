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
    public class EditDeceased(UserManager<AppUser> userManager, XContext dContext, IDataProtectionProvider provider) :ViewComponent
    {
        public readonly XContext dcx=dContext;
        public readonly IDataProtector protector = provider.CreateProtector("IdProtector");
        public readonly UserManager<AppUser> usm = userManager;
        public IViewComponentResult Invoke(string Id)

        {
            var decodedId=HttpUtility.UrlDecode(Id)?.Replace(" ","+");
            var decryptedId=protector.Unprotect(decodedId);
            if(!Guid.TryParse(decryptedId,out Guid deceasedGuid))
            {

                return View("Error", "Invalid Excuse Duty Id format");
            }
            
          var  deceasedToEdit = (from m in dcx.Deceased where m.PublicId == deceasedGuid select m ).FirstOrDefault();

            EditDeceasedVM editDeceasedVM = new()
            {
                UsersList = new SelectList(usm.Users.ToList(), (nameof(AppUser.Id),nameof(AppUser.Fullname))),
                DeceasedTypes = new SelectList(dcx.DeceasedTypes.ToList(), "DeceasedTypeId", "Code"),
                DeceasedId=deceasedToEdit.PublicId,
                Depositor=deceasedToEdit.Depositor,
                DepositorAddress=deceasedToEdit.DepositorAddress,
                Deceased=deceasedToEdit.Name,
                FolderNo=deceasedToEdit.FolderNo,
                TagNo=deceasedToEdit.TagNo,
                Diagnoses=deceasedToEdit.Diagnoses,
                WardInCharge=deceasedToEdit.WardInCharge,
                DeceasedTypeId=deceasedToEdit.DeceasedTypeId,
                 SelectedUsers = (from x in dcx.DeceasedAssignments where x.PublicId == deceasedGuid select x.UserId).ToList(),

            };
            

            return View(editDeceasedVM);
        }
    }
}
