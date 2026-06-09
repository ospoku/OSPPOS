using Microsoft.AspNetCore.Mvc;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{
    public class ViewStocks:ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            ViewStocksVM viewStocksVM = new()
            {

            };

            return View(viewStocksVM);
        }
    }
}
