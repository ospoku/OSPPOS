using Microsoft.AspNetCore.Mvc;
using OSPPOS.Data;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{
    public class ViewSaleOrders(XContext ctx):ViewComponent
    {
        public IViewComponentResult Invoke()
        {
           

            var sale = ctx.SaleOrders.Select(s => new ViewSalesVM {CustomerName=s.Customer.Name, OrderDate=s.OrderDate,OrderNumber=s.OrderNumber }).ToList();

            return View(sale);
        }
    }
}
