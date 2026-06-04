
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Data;
using OSPPOS.Interfaces;

namespace OSPPOS.Controllers;

[Authorize]
public class StockController : Controller
{
    private readonly IStockService _stock;
    private readonly XContext _db;

    public StockController(IStockService stock, XContext db)
    {
        _stock = stock;
        _db = db;
    }

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
    public async Task<IActionResult> Create()
    {
        await PopulateDropDownsAsync();
        return View(new CreateStockBatchVm { ReceivedDate = DateTime.Today });
    }

    // POST /Stock/Create
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateStockBatchVm vm)
    {
        vm.Items.RemoveAll(i => i.ProductId == 0 || i.Quantity == 0);
        if (!vm.Items.Any())
            ModelState.AddModelError("", "Add at least one product.");

        if (!ModelState.IsValid)
        {
            await PopulateDropDownsAsync();
            return View(vm);
        }

        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value;
        var grn = await _stock.CreateGRNAsync(vm, userId);
        TempData["Success"] = $"GRN {grn.GRNNumber} recorded successfully.";
        return RedirectToAction(nameof(Details), new { id = grn.Id });
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
}
