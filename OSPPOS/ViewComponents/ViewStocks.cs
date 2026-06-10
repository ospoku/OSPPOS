using Microsoft.AspNetCore.Mvc;
using OSPPOS.Data;
using OSPPOS.Models;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{
    public class ViewStocks(XContext ctx):ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            ViewStocksVM viewStocksVM = new()
            {
                StockBatchItems= ctx.StockBatchItems.ToList(),
                
            };
    

            return View(viewStocksVM);
        }
    }
}
