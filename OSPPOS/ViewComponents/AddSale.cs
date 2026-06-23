using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Data;
using OSPPOS.Models;
using OSPPOS.ViewModels;
using System.Reflection.Metadata.Ecma335;

namespace OSPPOS.ViewComponents
{
    public class AddSale(XContext ctx) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            AddSaleVM addSaleVM = new()
            {

                Customers = new SelectList(ctx.Customers.Where(c => c.IsActive && c.AllowCredit)
            .OrderBy(c => c.Name),nameof(Customer.CustomerId),nameof(Customer.Name)),
         

                Products = await ctx.Products
            .Include(p => p.Category)
            .Where(p => p.IsActive && p.CurrentStock > 0)
            .OrderBy(p => p.Category.Name)
            .ThenBy(p => p.Name)
            .ToListAsync()
            };

            return View(addSaleVM);
        }

       
    }
}

    
