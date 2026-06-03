using Microsoft.AspNetCore.Mvc;

namespace DMX.ViewComponents
{
    public class SystemSetup:ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();  
        }
    }
}
