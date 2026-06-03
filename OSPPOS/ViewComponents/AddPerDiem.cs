using Microsoft.AspNetCore.Mvc;

namespace DMX.ViewComponents
{
    public class AddPerDiem:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
