
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Data;
using OSPPOS.Interfaces;
using OSPPOS.Models;
using OSPPOS.ViewModels;
using System.Security.Claims;
namespace OSPPOS.Controllers;

[Authorize]
public class StockController(IStockService stock, XContext ctx) : Controller
{
    private readonly IStockService _stock = stock;


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
    public async Task<IActionResult> AddStockBatch(AddStockBatchVM vm)
    {
        if (!ModelState.IsValid)
        {
            return ViewComponent(nameof(AddStockBatch), new { vm });
        }

        var batch = new StockBatch
        {
            GRNNumber = await GenerateGRNNumberAsync(),
            SupplierId = vm.SupplierId,
            ReceivedDate = vm.ReceivedDate,
            SupplierInvoiceRef = vm.SupplierInvoiceRef,
            Notes = vm.Notes,
            ReceivedById = User.FindFirstValue(ClaimTypes.NameIdentifier)!
        };

        foreach (var item in vm.Items)
        {
            batch.Items.Add(new StockBatchItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitCost = item.UnitCost,
                ExpiryDate = item.ExpiryDate
            });

            // Update running stock on the product
            var product = await ctx.Products.FindAsync(item.ProductId);
            if (product != null)
                product.CurrentStock += item.Quantity;
        }

        ctx.StockBatches.Add(batch);
        await ctx.SaveChangesAsync();

        return RedirectToAction(nameof(ViewStocks));
    }

    private async Task<string> GenerateGRNNumberAsync()
    {
        var count = await ctx.StockBatches.CountAsync();
        return $"GRN-{DateTime.UtcNow.Year}-{(count + 1):D3}";
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
            await ctx.Suppliers.Where(s => s.IsActive).OrderBy(s => s.Name).ToListAsync(),
            "Id", "Name");
        ViewBag.Products = await ctx.Products
            .Include(p => p.Category)
            .Where(p => p.IsActive)
            .OrderBy(p => p.Category.Name).ThenBy(p => p.Name)
            .Select(p => new { p.ProductId, Name = $"{p.Name} ({p.SKU}) – Stock: {p.CurrentStock}", p.CostPrice })
            .ToListAsync();
    }

    public IActionResult ViewStocks()
    {
        return ViewComponent(nameof(ViewStocks));
    }
}
