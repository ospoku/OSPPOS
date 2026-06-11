using Microsoft.AspNetCore.Mvc;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{
    public class AddSale:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            AddSaleVM addSaleVM = new();

            return View(addSaleVM);
        }
    }
}
