using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Data;
using static OSPPOS.Services.ReportService;

namespace OSPPOS.Controllers;

[Authorize]
public class InventoryController(XContext db, Services.ReportService.IReportService reports) : Controller
{
    private readonly XContext ctx = db;
    private readonly IReportService _reports = reports;

    // Stock levels overview
    public async Task<IActionResult> Index(int? categoryId, string? search)
    {
        var q = ctx.Products
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .Where(p => p.IsActive)
            .AsQueryable();

        if (categoryId.HasValue) q = q.Where(p => p.CategoryId == categoryId.Value);
        if (!string.IsNullOrWhiteSpace(search))
            q = q.Where(p => p.Name.Contains(search) || p.SKU.Contains(search));

        ViewBag.Categories = new SelectList(
            await ctx   .Categories.Where(c => c.IsActive).ToListAsync(), "Id", "Name", categoryId);
        ViewBag.Search = search;
        ViewBag.SelectedCategory = categoryId;
        return View(await q.OrderBy(p => p.Category.Name).ThenBy(p => p.Name).ToListAsync());
    }

    // Stock movement for a single product
    public async Task<IActionResult> Movement(int id)
    {
        var product = await ctx.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        if (product is null) return NotFound();

        var batches = await ctx.StockBatchItems
            .Include(i => i.StockBatch).ThenInclude(b => b.Supplier)
            .Where(i => i.ProductId == id)
            .OrderByDescending(i => i.StockBatch.ReceivedDate)
            .ToListAsync();

        var sales = await ctx.SaleOrderItems
            .Include(i => i.SaleOrder).ThenInclude(o => o.Customer)
            .Where(i => i.ProductId == id)
            .OrderByDescending(i => i.SaleOrder.OrderDate)
            .ToListAsync();

        ViewBag.Product = product;
        ViewBag.Batches = batches;
        ViewBag.Sales = sales;
        return View();
    }
}

