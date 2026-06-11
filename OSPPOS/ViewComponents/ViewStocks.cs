using Microsoft.AspNetCore.Mvc;
using OSPPOS.Data;
using OSPPOS.Models;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{
    public class ViewStocks(XContext ctx) : ViewComponent
    {
       

        public IViewComponentResult Invoke()
        {
            List<ViewStocksVM> stocks = ctx.StockBatchItems.Select(s => new ViewStocksVM { }).ToList();
    

            return View(stocks);
        }
    }
}
