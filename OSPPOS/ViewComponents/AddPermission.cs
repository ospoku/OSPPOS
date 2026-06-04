
using Microsoft.AspNetCore.Mvc;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{
    public class AddPermission:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var permissionVM = new AddPermissionVM()
            {

            };


            return View(permissionVM);
        }
    }
}
