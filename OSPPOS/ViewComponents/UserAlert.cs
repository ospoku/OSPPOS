using Microsoft.AspNetCore.Mvc;


namespace OSPPOS.ViewComponents
{
    public class UserAlert: Microsoft.AspNetCore.Mvc.ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
