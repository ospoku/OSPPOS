using Microsoft.AspNetCore.Mvc;

namespace DMX.ViewComponents
{
    public class ViewPreferences : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
