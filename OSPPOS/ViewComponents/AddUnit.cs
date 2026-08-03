using Microsoft.AspNetCore.Mvc;

namespace OSPPOS.ViewComponents
{
    public class AddUnit:ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
