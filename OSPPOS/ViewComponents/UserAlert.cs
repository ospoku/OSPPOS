using Microsoft.AspNetCore.Mvc;


namespace OSPPOS.ViewComponents
{
    public class UserAlert:ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
