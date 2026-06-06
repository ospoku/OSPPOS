using Microsoft.AspNetCore.Mvc;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{
    public class AddProduct:ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            AddProductVM addProductVM = new() { };

            return View(addProductVM);
        }
    }
}
