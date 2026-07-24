using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Data;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{

    public class ViewPatients(XContext ctx) : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        //        public IViewComponentResult Invoke()
        //        {
        //            var products = ctx.Products
        //                .Include(p => p.Category)
        //                .Include(p => p.Supplier)
        //                .OrderBy(p => p.Category.Name)
        //                .ThenBy(p => p.Name)
        //                .Select(p => new ViewProductsVM {Category=p.Category.Name,ReorderLevel=p.ReorderLevel,IsActive=p.IsActive, WholesalePrice=p.WholesalePrice,Unit=p.Unit.Name, Name=p.Name,TotalProducts }).ToList();



        //            return View(products);
        //        }
        //    }
        //}
        public IViewComponentResult Invoke()
        {
            var productList = ctx.Patients
             
                .ToList();

            var result = productList.Select(p => new ViewPatientsVM
            {
          
            }).ToList();

            return View(result);
        }
    }
}