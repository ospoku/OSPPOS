using DMX.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DMX.ViewComponents
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
