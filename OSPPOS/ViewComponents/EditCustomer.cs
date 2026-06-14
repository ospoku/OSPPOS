using Microsoft.AspNetCore.Mvc;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{
    public class EditCustomer : ViewComponent
    {
      
        public IViewComponentResult Invoke()

        {
            EditCustomerVM addSupplierVM = new() { };


            return View(addSupplierVM);
        }

    }
}
