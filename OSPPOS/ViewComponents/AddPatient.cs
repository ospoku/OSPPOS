using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OSPPOS.Data;
using OSPPOS.Models;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{
    public class AddPatient(XContext ctx) : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        public IViewComponentResult Invoke(AddPatientVM? addProductVM = null)
        {
            addProductVM ??= new AddPatientVM();

            // Populate select lists on whatever model w
          
            return View(addProductVM);
        }
    }
}
