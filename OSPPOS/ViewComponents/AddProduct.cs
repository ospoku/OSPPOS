using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OSPPOS.Data;
using OSPPOS.Models;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{
    public class AddProduct(XContext ctx) : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        public IViewComponentResult Invoke(AddProductVM? addProductVM = null)
        {
            addProductVM ??= new AddProductVM();

            // Populate select lists on whatever model we have
            addProductVM.CategoryList = new SelectList(ctx.Categories.ToList(), nameof(Category.CategoryId), nameof(Category.Name));
            addProductVM.SupplierList = new SelectList(ctx.Suppliers.ToList(), nameof(Supplier.SupplierId), nameof(Supplier.Name));
            addProductVM.UnitList = new SelectList(ctx.Units.ToList(), nameof(Unit.UnitId), nameof(Unit.Name));
            

            return View(addProductVM);
        }
    }
}
