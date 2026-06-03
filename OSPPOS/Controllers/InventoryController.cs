using DrinksPOS.Data;
using DrinksPOS.Models;
using DrinksPOS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DrinksPOS.Controllers;

[Authorize]
public class InventoryController : Controller
{
    private readonly AppDbContext _db;
    private readonly IReportService _reports;

    public InventoryController(AppDbContext db, IReportService reports)
    {
        _db = db;
        _reports = reports;
    }

    // Stock levels overview
    public async Task<IActionResult> Index(int? categoryId, string? search)
    {
        var q = _db.Products
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .Where(p => p.IsActive)
            .AsQueryable();

        if (categoryId.HasValue) q = q.Where(p => p.CategoryId == categoryId.Value);
        if (!string.IsNullOrWhiteSpace(search))
            q = q.Where(p => p.Name.Contains(search) || p.SKU.Contains(search));

        ViewBag.Categories = new SelectList(
            await _db.Categories.Where(c => c.IsActive).ToListAsync(), "Id", "Name", categoryId);
        ViewBag.Search = search;
        ViewBag.SelectedCategory = categoryId;
        return View(await q.OrderBy(p => p.Category.Name).ThenBy(p => p.Name).ToListAsync());
    }

    // Stock movement for a single product
    public async Task<IActionResult> Movement(int id)
    {
        var product = await _db.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        if (product is null) return NotFound();

        var batches = await _db.StockBatchItems
            .Include(i => i.StockBatch).ThenInclude(b => b.Supplier)
            .Where(i => i.ProductId == id)
            .OrderByDescending(i => i.StockBatch.ReceivedDate)
            .ToListAsync();

        var sales = await _db.SaleOrderItems
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

[Authorize(Roles = "Admin,Manager")]
public class ProductsController : Controller
{
    private readonly AppDbContext _db;
    public ProductsController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index() =>
        View(await _db.Products.Include(p => p.Category).Include(p => p.Supplier)
            .OrderBy(p => p.Category.Name).ThenBy(p => p.Name).ToListAsync());

    public async Task<IActionResult> Create()
    {
        await PopulateDropDownsAsync();
        return View(new Product());
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product model)
    {
        if (!ModelState.IsValid)
        {
            await PopulateDropDownsAsync();
            return View(model);
        }
        // Generate SKU if empty
        if (string.IsNullOrWhiteSpace(model.SKU))
            model.SKU = await GenerateSKUAsync(model.CategoryId);

        _db.Products.Add(model);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Product created.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var p = await _db.Products.FindAsync(id);
        if (p is null) return NotFound();
        await PopulateDropDownsAsync();
        return View(p);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Product model)
    {
        if (!ModelState.IsValid)
        {
            await PopulateDropDownsAsync();
            return View(model);
        }
        _db.Products.Update(model);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Product updated.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleActive(int id)
    {
        var p = await _db.Products.FindAsync(id);
        if (p is null) return NotFound();
        p.IsActive = !p.IsActive;
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateDropDownsAsync()
    {
        ViewBag.Categories = new SelectList(
            await _db.Categories.Where(c => c.IsActive).ToListAsync(), "Id", "Name");
        ViewBag.Suppliers = new SelectList(
            await _db.Suppliers.Where(s => s.IsActive).ToListAsync(), "Id", "Name");
    }

    private async Task<string> GenerateSKUAsync(int categoryId)
    {
        var cat = await _db.Categories.FindAsync(categoryId);
        var prefix = cat?.Name[..Math.Min(3, cat.Name.Length)].ToUpper() ?? "PRD";
        var count = await _db.Products.CountAsync(p => p.CategoryId == categoryId);
        return $"{prefix}-{(count + 1):D4}";
    }
}

[Authorize(Roles = "Admin,Manager")]
public class SuppliersController : Controller
{
    private readonly AppDbContext _db;
    public SuppliersController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index() =>
        View(await _db.Suppliers.OrderBy(s => s.Name).ToListAsync());

    public IActionResult Create() => View(new Supplier());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Supplier model)
    {
        if (!ModelState.IsValid) return View(model);
        _db.Suppliers.Add(model);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Supplier added.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var s = await _db.Suppliers.FindAsync(id);
        if (s is null) return NotFound();
        return View(s);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Supplier model)
    {
        if (!ModelState.IsValid) return View(model);
        _db.Suppliers.Update(model);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Supplier updated.";
        return RedirectToAction(nameof(Index));
    }
}

[Authorize(Roles = "Admin,Manager")]
public class CustomersController : Controller
{
    private readonly AppDbContext _db;
    public CustomersController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index() =>
        View(await _db.Customers
            .Include(c => c.SaleOrders).ThenInclude(o => o.Payments)
            .OrderBy(c => c.Name).ToListAsync());

    public IActionResult Create() => View(new Customer());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Customer model)
    {
        if (!ModelState.IsValid) return View(model);
        _db.Customers.Add(model);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Customer created.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var c = await _db.Customers.FindAsync(id);
        if (c is null) return NotFound();
        return View(c);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Customer model)
    {
        if (!ModelState.IsValid) return View(model);
        _db.Customers.Update(model);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Customer updated.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Statement(int id)
    {
        var c = await _db.Customers
            .Include(x => x.SaleOrders).ThenInclude(o => o.Items).ThenInclude(i => i.Product)
            .Include(x => x.SaleOrders).ThenInclude(o => o.Payments)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (c is null) return NotFound();
        return View(c);
    }
}
