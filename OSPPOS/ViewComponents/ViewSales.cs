using Microsoft.AspNetCore.Mvc;
using OSPPOS.Data;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{
    public class ViewSales(XContext ctx):ViewComponent
    {
        public IViewComponentResult Invoke()
        {
           

            var sale = ctx.SaleOrders.Select(s => new ViewSalesVM {AmountDue=s.AmountDue,AmountPaid=s.AmountPaid,SaleType=s.SaleType.Name}).ToList();

            return View(sale);
        }
    }
}
