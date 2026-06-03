using DMX.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DMX.ViewComponents
{
    public class AddTransportMode:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var addTransportModeVM = new AddTransportModeVM();    

            return View(addTransportModeVM);
        }
    }
}
