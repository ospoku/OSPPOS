using Microsoft.AspNetCore.Mvc;

namespace OSPPOS.ViewComponents
{
    public class Finance:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
