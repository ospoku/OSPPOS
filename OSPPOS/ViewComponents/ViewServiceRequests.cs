using Microsoft.AspNetCore.Mvc;
using DMX.Data;
using DMX.ViewModels;
using Microsoft.AspNetCore.Identity;
using DMX.Models;
namespace DMX.ViewComponents
{
    public class ViewServiceRequests(XContext dContext, UserManager<AppUser>userManager) : ViewComponent
    {
        public readonly XContext dcx = dContext;
        public readonly  UserManager<AppUser> usm=userManager;

        public IViewComponentResult Invoke()
        {
            var user = usm.GetUserAsync(HttpContext.User).Result?.Id;
            var servList = dcx.ServiceAssignments.Where(a => a.AppUser.Id == user || a.ServiceRequest.CreatedBy == user).Select(a => new 
             ViewServiceRequestsVM
            {
                PublicId=a.PublicId,
                Title=a.ServiceRequest.Title,
          Description=a.ServiceRequest.Description,
           
            RequestNumber = a.ServiceRequest.RequestNumber,
          CreatedDate=a.ServiceRequest.CreatedDate.Value,
              
            }).OrderByDescending(t => t.CreatedDate).ToList();
            return View(servList);
        }
    }
}
