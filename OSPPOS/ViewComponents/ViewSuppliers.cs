using Microsoft.AspNetCore.Mvc;
using OSPPOS.Data;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{
    public class ViewSuppliers(XContext ctx):ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            
           
            
    

            var sup = ctx.Suppliers.OrderBy(s=>s.Name).Select(s=> new ViewSuppliersVM { }).ToList();

            return View(sup);
        }
    }
}
