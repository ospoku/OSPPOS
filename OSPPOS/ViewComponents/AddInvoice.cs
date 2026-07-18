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

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await ctx.Products
              .Include(p => p.Category).Include(p=>p.Unit)
              .Where(p => p.IsActive && p.CurrentStock > 0)
              .OrderBy(p => p.Category.Name)
              .ThenBy(p => p.Name)
              .ToListAsync();
            
            AddInvoiceVM addInvoiceVM = new()
            {
                Items = [],
                Products = products,

                //Customers = [.. ctx.Customers.Select(c => new CustomerSelectList() { AllowCredit=c.AllowCredit,CreditLimit=c.CreditLimit,CustomerId=c.CustomerId })]
           Customers= new SelectList(ctx.Customers.Select(c=> new CustomerSelectList() {CustomerId=c.CustomerId,Name=c.Name }).ToList(),nameof(Customer.CustomerId),nameof(Customer.Name))
            };
 
            return View(addInvoiceVM);
        }
    }
}
