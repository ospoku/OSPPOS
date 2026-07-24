using Microsoft.AspNetCore.Mvc;

namespace DMX.ViewComponents
{
    public class ManageUserPermissions : ViewComponent
    {


        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
