using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Data;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{

    public class ViewInvoices(XContext ctx) : ViewComponent
    {
   
        public IViewComponentResult Invoke()
        {
            var productList = ctx.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Include(p => p.Unit)
                .OrderBy(p => p.Category.Name)
                .ThenBy(p => p.Name)
                .ToList();

            var result = productList.Select(p => new ViewProductsVM
            {
                Name = p.Name,
                Category = p.Category.Name,
                Unit = p.Unit.Name,
                WholesalePrice = p.WholesalePrice,
                ReorderLevel = p.ReorderLevel,
                IsActive = p.IsActive,
                CurrentStock=p.CurrentStock,
                TotalProducts = productList.Count
            }).ToList();

            return View(result);
        }
    }
}