using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Data;
using OSPPOS.Interfaces;
using OSPPOS.ViewComponents;


namespace OSPPOS.Controllers;

[Authorize]
public class InventoryController(XContext db, IReportService report) : Controller
{
    private readonly XContext ctx = db;
    private readonly IReportService reports = report;




    public  IActionResult ViewInventory()
    {
        return ViewComponent(nameof(ViewInventory));
    }

    // Stock levels overview
 

    // Stock movement for a single product
    public async Task<IActionResult> Movement(int id)
    {
        var product = await ctx.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductId == id);
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

