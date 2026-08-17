using Microsoft.AspNetCore.Mvc;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{
    public class AddUnit:ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            AddUnitVM addUnitVM = new() { }; 


            return View(addUnitVM);
        }
    }
}
