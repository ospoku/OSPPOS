using Microsoft.AspNetCore.Mvc;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{
    public class EditSupplier : ViewComponent
    {
      
        public IViewComponentResult Invoke()

        {
            AddSupplierVM addSupplierVM = new() { };


            return View(addSupplierVM);
        }

    }
}
