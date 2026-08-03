using Microsoft.AspNetCore.Mvc;

namespace OSPPOS.ViewComponents
{
    public class ViewActivities:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
