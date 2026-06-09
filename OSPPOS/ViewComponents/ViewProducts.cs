using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Data;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{

    public class ViewProducts(XContext ctx):ViewComponent
    {


        public IViewComponentResult Invoke()
        {
           



            var products = ctx.Products
                           .Include(p => p.Category)
                           .Include(p => p.Supplier)
                           .OrderBy(p => p.Category.Name)
                           .ThenBy(p => p.Name).ToList();
            ViewProductsVM viewProductsVM = new()
            {
                Products=products,
                TotalProducts=products.Count(),
            };

            return View(viewProductsVM);
        }
    }
}
