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

                Customers = await ctx.Customers
            .Where(c => c.IsActive)
            .OrderBy(c => c.Name)
            .ToListAsync(),

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

    
