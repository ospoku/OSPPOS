
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Data;
using OSPPOS.Interfaces;
using OSPPOS.ViewComponents;
using OSPPOS.ViewModels;
using System.Security.Claims;

namespace OSPPOS.Controllers;

[Authorize]
public class StockController(IStockService stock, XContext db) : Controller
{
    private readonly IStockService _stock = stock;
    private readonly XContext _db = db;

    // GET /Stock
    public async Task<IActionResult> Index(DateTime? from, DateTime? to)
    {
        from ??= DateTime.Today.AddDays(-30);
        to ??= DateTime.Today;
        var grns = await _stock.GetAllGRNsAsync(from, to);
        ViewBag.From = from.Value.ToString("yyyy-MM-dd");
        ViewBag.To = to.Value.ToString("yyyy-MM-dd");
        return View(grns);
    }

    // GET /Stock/Create
    public async Task<IActionResult> AddStock()
    {
        await PopulateDropDownsAsync();
        return View(new AddStockBatchVM { ReceivedDate = DateTime.Today });
    }


    
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddStock(AddStockVM addStockVM)
        {
            if (!ModelState.IsValid)
            {
                return ViewComponent(nameof(AddStock), new { addStockVM });
            }

            // Find the product and update its stock
            var product = await ctx.Products.FindAsync(addStockVM.ProductId);
            if (product == null)
            {
                ModelState.AddModelError("ProductId", "Product not found");
                return ViewComponent(nameof(AddStock), new { addStockVM });
            }

            // Record the stock entry
            var stockEntry = new Stock
            {
                ProductId = addStockVM.ProductId,
                SupplierId = addStockVM.SupplierId,
                Quantity = addStockVM.Quantity,
                CostPrice = addStockVM.CostPrice,
                Notes = addStockVM.Notes,
                DateAdded = DateTime.UtcNow,
                AddedByUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            product.CurrentStock += addStockVM.Quantity;

            ctx.StockEntries.Add(stockEntry);
            await ctx.SaveChangesAsync();

            return RedirectToAction(nameof(ViewProducts));
        }
    

    // GET /Stock/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var grn = await _stock.GetGRNAsync(id);
        if (grn is null) return NotFound();
        return View(grn);
    }

    private async Task PopulateDropDownsAsync()
    {
        ViewBag.Suppliers = new SelectList(
            await _db.Suppliers.Where(s => s.IsActive).OrderBy(s => s.Name).ToListAsync(),
            "Id", "Name");
        ViewBag.Products = await _db.Products
            .Include(p => p.Category)
            .Where(p => p.IsActive)
            .OrderBy(p => p.Category.Name).ThenBy(p => p.Name)
            .Select(p => new { p.Id, Name = $"{p.Name} ({p.SKU}) – Stock: {p.CurrentStock}", p.CostPrice })
            .ToListAsync();
    }

    public IActionResult ViewStocks()
    {
        return ViewComponent(nameof(ViewStocks));
    }
}
