using Microsoft.AspNetCore.Mvc;

namespace OSPPOS.ViewComponents
{
    public class Backup:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
