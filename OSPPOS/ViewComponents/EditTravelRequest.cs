using DMX.Data;

using DMX.Models;
using DMX.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared;
using Microsoft.EntityFrameworkCore;

namespace DMX.ViewComponents
{
    public class EditTravelRequest(XContext dContext, UserManager<AppUser> userManager, IDataProtectionProvider provider) : ViewComponent
    {
        public readonly XContext dcx = dContext;
        public readonly UserManager<AppUser> usm = userManager; public readonly IDataProtector protector = provider.CreateProtector("IdProtector");
        public IViewComponentResult Invoke(string TravelRequestId)


        {
           

            TravelRequest travelRequestToEdit = new TravelRequest();
            travelRequestToEdit = (from tr in dcx.TravelRequests.Include(m => m.Comments.OrderBy(m=>m.CreatedDate)) where tr.TravelRequestId==@protector.Unprotect(TravelRequestId )select tr ).FirstOrDefault();

            EditTravelRequestVM editTravelRequestVM = new EditTravelRequestVM
            {
                Purpose = travelRequestToEdit.Purpose,
                StartDate = travelRequestToEdit.StartDate,
                EndDate = travelRequestToEdit.EndDate,
                TransportModes = new SelectList(dcx.TransportTypes.ToList(), nameof(TransportType.TransportTypeId), nameof(TransportType.Name)),
                UsersList = new SelectList(usm.Users.ToList(), nameof(AppUser.Id), nameof(AppUser.Fullname)),
                TravelTypes = new SelectList(dcx.TravelTypes, nameof(TravelType.TravelTypeId), nameof(TravelType.Name))
            };
            

            return View(editTravelRequestVM);
        }
    }
}
