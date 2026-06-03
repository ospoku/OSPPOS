using DMX.Data;
using DMX.Models;
using DMX.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Xml.Linq;

namespace DMX.ViewComponents
{
    public class AddServiceRequest:ViewComponent
    {
        public readonly UserManager<AppUser> usm;
        public readonly XContext xct;
        public AddServiceRequest(UserManager<AppUser> userManager,XContext context)
        {
            usm = userManager;
            xct = context;
        }
        public IViewComponentResult Invoke()
        {
            AddServiceRequestVM addServiceRequest = new AddServiceRequestVM
            {
                UsersList = new SelectList(usm.Users.ToList(), nameof(AppUser.Id),nameof(AppUser.Fullname)),
                Status=new SelectList(xct.Statuses.ToList(), nameof(Status.Id),nameof(Status.Name)),
                Categories =  new SelectList( xct.Categories.ToList(),nameof(Category.Id),nameof(Category.Name)),
                RequestTypes=new SelectList (xct.RequestTypes.ToList(),nameof(RequestType.Id),nameof(RequestType.Name)),
                Urgency= new SelectList (xct.Priorities.ToList(),nameof(Priority.Id),nameof(Priority.Name))
            };
            return View(addServiceRequest);
        }
    }
}
