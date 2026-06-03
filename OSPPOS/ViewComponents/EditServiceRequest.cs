using DMX.Data;

using DMX.Models;
using DMX.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace DMX.ViewComponents
{
    public class EditServiceRequest:ViewComponent
    {
        public readonly XContext dcx;
        public readonly IDataProtector protector;
        public EditServiceRequest(XContext dContext,IDataProtectionProvider provider)
        {
            dcx = dContext;  
            protector = provider.CreateProtector("IdProtector");
        }

        public IViewComponentResult Invoke(string Id)
        {
          
            var decryptedId=protector.Unprotect(Id);
            if(!Guid.TryParse(decryptedId, out Guid requestGuid))
            {

            }

            ServiceRequest serviceRequestToEdit = new ServiceRequest();
            serviceRequestToEdit = (from sr in dcx.ServiceRequests.Include(sr => sr.Comments.OrderBy(m=>m.CreatedDate)) where sr.PublicId ==requestGuid select sr ).FirstOrDefault();

            EditServiceRequestVM editServiceRequestVM = new EditServiceRequestVM
            {
           


            };
            

            return View(editServiceRequestVM);
        }
    }
}
