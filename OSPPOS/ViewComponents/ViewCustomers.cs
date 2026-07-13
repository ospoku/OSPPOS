using Microsoft.AspNetCore.Mvc;
using OSPPOS.Data;
using OSPPOS.Enums;
using OSPPOS.Models;
using OSPPOS.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace OSPPOS.ViewComponents
{
    public class ViewCustomers(XContext ctx):ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cust = ctx.Customers.OrderBy(c => c.Name).Select(c => new ViewCustomersVM() { CustomerId =c.CustomerId, 
                Name=c.Name ,
                Phone=c.Phone,
                Email=c.Email, 
                Address=c.Address, 
                TaxNumber =c.TaxNumber, 
                CreditLimit =c.CreditLimit, 
                AllowCredit =c.AllowCredit,
                IsActive =c.IsActive,  
                SaleOrders =c.SaleOrders.Select(s=>s.OrderNumber).ToList(), 
                Payments=c.Payments,
 
}).ToList();
            return View(cust);
        }
    }
}
