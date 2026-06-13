using Microsoft.AspNetCore.Mvc;
using OSPPOS.Data;
using OSPPOS.Models;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{
    public class AddStockBatch(XContext ctx) : ViewComponent
    {
        public IViewComponentResult Invoke(AddStockBatchVM vm)
        {
            vm ??= new AddStockBatchVM();

            vm.SupplierList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
                ctx.Suppliers.ToList(),
                nameof(Supplier.SupplierId),
                nameof(Supplier.Name));

            vm.ProductList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
                ctx.Products.Where(p => p.IsActive).ToList(),
                nameof(Product.ProductId),
                nameof(Product.Name));

            return View(vm);
        }
    }
}
