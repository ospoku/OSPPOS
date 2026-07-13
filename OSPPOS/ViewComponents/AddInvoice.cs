using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Data;
using OSPPOS.Models;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{
    public class AddInvoice(XContext ctx):ViewComponent
    {

        public IViewComponentResult Invoke()
        {

            AddInvoiceVM addInvoiceVM = new()
            {
                Items = [],

                //Customers = [.. ctx.Customers.Select(c => new CustomerSelectList() { AllowCredit=c.AllowCredit,CreditLimit=c.CreditLimit,CustomerId=c.CustomerId })]
           Customers= new SelectList(ctx.Customers.Select(c=> new CustomerSelectList() {CustomerId=c.CustomerId,Name=c.Name }).ToList(),nameof(Customer.CustomerId),nameof(Customer.Name))
            };
 
            return View(addInvoiceVM);
        }
    }
}
