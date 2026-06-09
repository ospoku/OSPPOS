using Microsoft.AspNetCore.Mvc;

namespace OSPPOS.ViewComponents
{
    public class AddStock:ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
