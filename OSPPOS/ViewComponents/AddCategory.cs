using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OSPPOS.Data;
using OSPPOS.Models;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{
    public class AddCategory : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
           

          
            return View();
        }
    }
}
