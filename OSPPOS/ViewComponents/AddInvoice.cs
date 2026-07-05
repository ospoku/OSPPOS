using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OSPPOS.Models;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{
    public class AddInvoice:ViewComponent
    {

        public IViewComponentResult Invoke()
        {

            AddInvoiceVM addInvoiceVM = new()
            {
                Items = []



            };
  
   
      
    

            return View(addInvoiceVM);
        }
    }
}
