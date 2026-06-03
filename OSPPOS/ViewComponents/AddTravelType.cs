using DMX.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DMX.ViewComponents
{
    public class AddTravelType:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var addTravelTypeVM = new AddTravelTypeVM();    

            return View(addTravelTypeVM);
        }
    }
}
