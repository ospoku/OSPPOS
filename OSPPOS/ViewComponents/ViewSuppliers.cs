using Microsoft.AspNetCore.Mvc;
using OSPPOS.Data;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{
    public class ViewSuppliers(XContext ctx):ViewComponent
    {
        public IViewComponentResult Invoke()
        { 
            var sup = ctx.Suppliers.OrderBy(s=>s.Name).Select(s=> new ViewSuppliersVM {Name=s.Name,
            Phone=s.Phone,Address=s.Address,Email=s.Email,PublicId=s.PublicId}).ToList();

            return View(sup);
        }
    }
}
