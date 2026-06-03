using Microsoft.AspNetCore.Mvc;

namespace DMX.ViewComponents
{
    public class UserManagement:ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();  
        }
    }
}
