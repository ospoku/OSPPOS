using Microsoft.AspNetCore.Mvc;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{
    public class AddCustomer(): ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            AddCustomerVM vm = new();

            return View(vm);
        }
    }
}
