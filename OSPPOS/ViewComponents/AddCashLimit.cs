using DMX.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DMX.ViewComponents
{
    public class AddCashLimit:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cashLimitVM = new AddCashLimitVM()
            {

            };


            return View(cashLimitVM);
        }
    }
}
