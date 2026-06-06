using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Data;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{
    public class ViewInventory(XContext ctx) : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync(int? categoryId, string? search)
        {
            var q = ctx.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Where(p => p.IsActive)
                .AsQueryable();

            if (categoryId.HasValue)
                q = q.Where(p => p.CategoryId == categoryId.Value);

            if (!string.IsNullOrWhiteSpace(search))
                q = q.Where(p => p.Name.ToLower().Contains(search.ToLower()) ||
                    p.SKU.ToLower().Contains(search.ToLower()));

            var products = await q
                .OrderBy(p => p.Category.Name)
                .ThenBy(p => p.Name)
                .ToListAsync();

            var inventoryVM = new ViewInventoryVM
            {
                Products = products,
                CategoryId = categoryId,
                Search = search
            };

            ViewBag.Categories = new SelectList(
                await ctx.Categories.Where(c => c.IsActive).ToListAsync(),
                "Id",
                "Name",
                categoryId
            );

            ViewBag.Search = search;
            ViewBag.SelectedCategory = categoryId;

            return View(inventoryVM);
        }
    }
}
