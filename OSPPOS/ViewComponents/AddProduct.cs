using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OSPPOS.Data;
using OSPPOS.Models;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{
    public class AddProduct(XContext ctx):ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            AddProductVM addProductVM = new()
            {
                CategoryList = new SelectList(ctx.Categories.ToList(), (nameof(Category.Id)), nameof(Category.Name)),
                SupplierList = new SelectList(ctx.Suppliers.ToList(), (nameof(Category.Id)), nameof(Category.Name)),
            UnitList=new SelectList(ctx.Units.ToList(), (nameof(Category.Id)), nameof(Unit.Name))
            };

            return View(addProductVM);
        }
    }
}
