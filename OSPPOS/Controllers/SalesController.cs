
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Data;
using OSPPOS.Interfaces;

namespace OSPPOS.Controllers;

[Authorize]
public class SalesController : Controller
{
    private readonly ISalesService _sales;
    private readonly XContext _db;

    public SalesController(ISalesService sales, XContext db)
    {
        _sales = sales;
        _db = db;
    }

    // GET /Sales
    public async Task<IActionResult> Index(DateTime? from, DateTime? to,
        PaymentStatus? status, SaleType? type)
    {
        from ??= DateTime.Today.AddDays(-30);
        to ??= DateTime.Today;
        var orders = await _sales.GetOrdersAsync(from, to, status, type);
        ViewBag.From = from.Value.ToString("yyyy-MM-dd");
        ViewBag.To = to.Value.ToString("yyyy-MM-dd");
        ViewBag.Status = status;
        ViewBag.Type = type;
        return View(orders);
    }

    // GET /Sales/Create  (POS screen)
    public async Task<IActionResult> Create()
    {
        await PopulateDropDownsAsync();
        return View(new CreateSaleVm());
    }

    // POST /Sales/Create
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateSaleVm vm)
    {
        vm.Items.RemoveAll(i => i.ProductId == 0 || i.Quantity == 0);
        if (!vm.Items.Any())
            ModelState.AddModelError("", "Add at least one item.");

        if (!ModelState.IsValid)
        {
            await PopulateDropDownsAsync();
            return View(vm);
        }

        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value;
        var (success, error, order) = await _sales.CreateSaleAsync(vm, userId);

        if (!success)
        {
            ModelState.AddModelError("", error);
            await PopulateDropDownsAsync();
            return View(vm);
        }

        TempData["Success"] = $"Sale {order!.OrderNumber} recorded.";
        return RedirectToAction(nameof(Receipt), new { id = order.Id });
    }

    // GET /Sales/Receipt/5
    public async Task<IActionResult> Receipt(int id)
    {
        var order = await _sales.GetOrderAsync(id);
        if (order is null) return NotFound();
        return View(order);
    }

    // GET /Sales/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var order = await _sales.GetOrderAsync(id);
        if (order is null) return NotFound();
        return View(order);
    }

    // GET /Sales/RecordPayment/5
    public async Task<IActionResult> RecordPayment(int id)
    {
        var order = await _sales.GetOrderAsync(id);
        if (order is null) return NotFound();
        ViewBag.Order = order;
        return View(new RecordPaymentVm { SaleOrderId = id, Amount = order.AmountDue });
    }

    // POST /Sales/RecordPayment
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> RecordPayment(RecordPaymentVm vm)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Order = await _sales.GetOrderAsync(vm.SaleOrderId);
            return View(vm);
        }

        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value;
        var (success, error) = await _sales.RecordPaymentAsync(vm, userId);
        if (!success)
        {
            ModelState.AddModelError("", error);
            ViewBag.Order = await _sales.GetOrderAsync(vm.SaleOrderId);
            return View(vm);
        }

        TempData["Success"] = "Payment recorded.";
        return RedirectToAction(nameof(Details), new { id = vm.SaleOrderId });
    }

    // AJAX: GET /Sales/GetProductPrice?productId=5
    [HttpGet]
    public async Task<IActionResult> GetProductPrice(int productId)
    {
        var p = await _db.Products.FindAsync(productId);
        if (p is null) return NotFound();
        return Json(new { p.SellingPrice, p.WholesalePrice, p.CurrentStock, p.Name, p.Unit });
    }

    private async Task PopulateDropDownsAsync()
    {
        ViewBag.Customers = new SelectList(
            await _db.Customers.Where(c => c.IsActive).OrderBy(c => c.Name).ToListAsync(),
            "Id", "Name");
        ViewBag.Products = await _db.Products
            .Include(p => p.Category)
            .Where(p => p.IsActive && p.CurrentStock > 0)
            .OrderBy(p => p.Category.Name).ThenBy(p => p.Name)
            .Select(p => new { p.Id, Name = $"{p.Name} ({p.SKU})", p.SellingPrice, p.CurrentStock })
            .ToListAsync();
    }
}
