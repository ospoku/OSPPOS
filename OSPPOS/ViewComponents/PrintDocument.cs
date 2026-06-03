using DMX.Data;

using DMX.Models;
using DMX.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace DMX.ViewComponents
{
    public class PrintDocument(XContext dContext, UserManager<AppUser> userManager, IDataProtector protector) : ViewComponent
    {


        public readonly XContext dcx = dContext;
        public readonly UserManager<AppUser> usm = userManager;
        public readonly IDataProtectionProvider provider = protector.CreateProtector("IdProtector");
        public IViewComponentResult Invoke(string Id)
        {
            var decodedId=HttpUtility.UrlDecode(Id).Replace(" ","+");
            var decryptedId=protector.Unprotect(decodedId);
            if(!Guid.TryParse(decryptedId, out Guid printGuid))
                {

            }
               Letter documentToEdit = new Letter();
            documentToEdit = (from d in dcx.Letters where d.PublicId == printGuid select d).FirstOrDefault();
            PrintDocumentVM printDocumentVM = new PrintDocumentVM
            {
                AdditionalNotes = documentToEdit.AdditionalNotes,
                Comments = (from c in dcx.LetterComments where c.LetterId == documentToEdit.Id select c).ToList(),
                ReferenceNumber = documentToEdit.ReferenceNumber,
                //SelectedUsers = AssignedUsers,               
            };
            return View(printDocumentVM);
        }
    }
}
