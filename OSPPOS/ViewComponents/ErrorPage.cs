using Microsoft.AspNetCore.Mvc;

namespace DMX.ViewComponents
{
    public class ErrorPage : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
