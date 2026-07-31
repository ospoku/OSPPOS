using Microsoft.AspNetCore.Mvc;
using OSPPOS.Data;
using OSPPOS.Enums;
using OSPPOS.Models;
using OSPPOS.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace OSPPOS.ViewComponents
{
    public class ViewCustomerSettings(XContext ctx): ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cust = ctx.Customers.OrderBy(c => c.Name).Select(c => new ViewCustomerSettingsVM() 
            { CustomerId =c.CustomerId, 
                Name=c.Name,
              
                CreditLimit =c.CreditLimit, 
                AllowCredit =c.AllowCredit,
                IsActive =c.IsActive,  
               
 
}).ToList();
            return View(cust);
        }
    }
}
