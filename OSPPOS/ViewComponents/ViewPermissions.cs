using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using OSPPOS.Models;

using OSPPOS.Data;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{
    public class ViewPermissions(XContext dContext) : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        public readonly XContext dcx = dContext;
        public IViewComponentResult Invoke()
        { 
            var perms = dcx.Permissions.Where(r => r.IsDeleted == false).Select(r => new ViewPermissionsVM
            {
           ActionName=r.Action,
           ModuleName=r.Module,
           Code=r.Code,
           
               Description=r.Description,
            

            }).ToList();

            return View(perms);
        }
    }
}
