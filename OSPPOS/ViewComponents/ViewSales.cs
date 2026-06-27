using Microsoft.AspNetCore.Mvc;
using OSPPOS.Data;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{
    public class ViewSales(XContext ctx):ViewComponent
    {
        public IViewComponentResult Invoke()
        {
           

            var sale = ctx.SaleOrders.Select(s => new ViewSalesVM {AmountDue=s.AmountDue,AmountPaid=s.AmountPaid,CustomerName=s.Customer.Name, OrderDate=s.OrderDate,OrderNumber=s.OrderNumber }).ToList();

            return View(sale);
        }
    }
}
