using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OSPPOS.Data;
using OSPPOS.Models;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{
    public class AddSale(XContext ctx):ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            AddSaleVM addSaleVM = new()
            {

                Customers= [.. ctx.Customers]

            };

            return View(addSaleVM);
        }
    }
}
