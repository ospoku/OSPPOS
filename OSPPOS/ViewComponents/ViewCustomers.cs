using Microsoft.AspNetCore.Mvc;
using OSPPOS.Data;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{
    public class ViewCustomers(XContext ctx):ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cust = ctx.Customers.OrderBy(c => c.Name).Select(c => new ViewCustomersVM() { }).ToList();
            return View(cust);
        }
    }
}
