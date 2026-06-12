using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OSPPOS.Data;
using OSPPOS.Models;
using OSPPOS.ViewModels;



    namespace OSPPOS.ViewComponents
    {
        public class AddStock(XContext ctx) : ViewComponent
        {
            public IViewComponentResult Invoke(AddStockVM addStockVM = null)
            {
                addStockVM ??= new AddStockVM();

                addStockVM.ProductList = new SelectList(
                    ctx.Products.Where(p => p.IsActive).ToList(),
                    nameof(Product.Id),
                    nameof(Product.Name));

                addStockVM.SupplierList = new SelectList(
                    ctx.Suppliers.ToList(),
                    nameof(Supplier.Id),
                    nameof(Supplier.Name));

                return View(addStockVM);
            }
        }
    }