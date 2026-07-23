using Microsoft.AspNetCore.Mvc;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{
    public class AddSupplier : Microsoft.AspNetCore.Mvc.ViewComponent
    {
      
        public IViewComponentResult Invoke()

        {
            AddSupplierVM addSupplierVM = new() { };


            return View(addSupplierVM);
        }

    }
}
