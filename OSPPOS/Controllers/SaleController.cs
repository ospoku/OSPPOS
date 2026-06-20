//using AspNetCoreHero.ToastNotification.Abstractions;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using OSPPOS.Data;
//using OSPPOS.Interfaces;
//using OSPPOS.Models;
//using OSPPOS.Services;
//using OSPPOS.ViewComponents;
//using OSPPOS.ViewModels;
//using System.Security.Claims;

//namespace OSPPOS.Controllers;

//[Authorize]
//public class SaleController(ISalesService sales, XContext ctx, EntityService entityService, INotyfService notyf) : Controller
//{
//    // GET /Sale
//    public async Task<IActionResult> Index(DateTime? from, DateTime? to,
//        PaymentStatus? status, SaleType? type)
//    {
//        from ??= DateTime.Today.AddDays(-30);
//        to ??= DateTime.Today;
//        var orders = await sales.GetOrdersAsync(from, to, status, type);
//        ViewBag.From = from.Value.ToString("yyyy-MM-dd");
//        ViewBag.To = to.Value.ToString("yyyy-MM-dd");
//        ViewBag.Status = status;
//        ViewBag.Type = type;
//        return View(orders);
//    }



//    // POST /Sale/AddSale
//    [HttpPost, ValidateAntiForgeryToken]
//    public async Task<IActionResult> AddSale(AddSaleVM vm)
//    {
//        // Remove empty lines
//        vm.Items.RemoveAll(i => i.ProductId == 0 || i.Quantity == 0);

//        if (vm.Items.Count == 0)
//        {
//            notyf.Error("Please add at least one item.");

//            return ViewComponent(nameof(ViewSales));
//        }

//        // Build the SaleOrder
//        var order = new SaleOrder
//        {
//            OrderNumber = await GenerateOrderNumberAsync(),
//            CustomerId = vm.CustomerId,
//            WalkInCustomerName = vm.WalkInCustomerName,
//            SaleType = vm.SaleType,
//            DueDate = vm.SaleType == SaleType.Crt ? vm.DueDate : null,
//            Notes = vm.Notes,
//            Discount = vm.Discount,
//            DiscountPercent = vm.DiscountPercent,
//            OrderDate = DateTime.UtcNow,
//            PaymentStatus = PaymentStatus.Unpaid,

//        };

//        // Map line items and reduce stock
//        foreach (var item in vm.Items)
//        {
//            order.Items.Add(new SaleOrderItem
//            {
//                ProductId = item.ProductId,
//                Quantity = item.Quantity,
//                UnitPrice = item.UnitPrice
//            });

//            var product = await ctx.Products.FindAsync(item.ProductId);
//            if (product != null)
//                product.CurrentStock -= item.Quantity;
//        }

//        // Record initial payment if cash was received
//        if (vm.CashReceived > 0)
//        {
//            order.Payments.Add(new Payment
//            {
//                Amount = vm.CashReceived,
//                PaymentMethod = vm.PaymentMethod,
//                Reference = vm.PaymentReference,


//            });

//            order.PaymentStatus = vm.CashReceived >= order.TotalAmount
//                ? PaymentStatus.Paid
//                : PaymentStatus.Partial;
//        }

//        bool result = await entityService.AddEntityAsync(order, User);

//        if (!result)
//        {
//            notyf.Error("Failed to add sale. Please try again.");

//            return ViewComponent(nameof(ViewSales));
//        }

//        notyf.Success($"Sale {order.OrderNumber} added successfully.");
//        return ViewComponent(nameof(ViewSales));
//    }

//    // GET /Sale/Receipt/5
//    public async Task<IActionResult> Receipt(int id)
//    {
//        var order = await sales.GetOrderAsync(id);
//        if (order is null) return NotFound();
//        return View(order);
//    }

//    // GET /Sale/Details/5
//    public async Task<IActionResult> Details(int id)
//    {
//        var order = await sales.GetOrderAsync(id);
//        if (order is null) return NotFound();
//        return View(order);
//    }

//    // GET /Sale/RecordPayment/5
//    public async Task<IActionResult> RecordPayment(int id)
//    {
//        var order = await sales.GetOrderAsync(id);
//        if (order is null) return NotFound();
//        ViewBag.Order = order;
//        return View(new RecordPaymentVm { SaleOrderId = id, Amount = order.AmountDue });
//    }

//    // POST /Sale/RecordPayment
//    [HttpPost, ValidateAntiForgeryToken]
//    public async Task<IActionResult> RecordPayment(RecordPaymentVm vm)
//    {
//        if (!ModelState.IsValid)
//        {
//            ViewBag.Order = await sales.GetOrderAsync(vm.SaleOrderId);
//            return View(vm);
//        }

//        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
//        var (success, error) = await sales.RecordPaymentAsync(vm, userId);

//        if (!success)
//        {
//            ModelState.AddModelError("", error);
//            ViewBag.Order = await sales.GetOrderAsync(vm.SaleOrderId);
//            return View(vm);
//        }

//        notyf.Success("Payment recorded.");
//        return RedirectToAction(nameof(Details), new { id = vm.SaleOrderId });
//    }

//    // AJAX: GET /Sale/GetProductPrice?productId=5
//    [HttpGet]
//    public async Task<IActionResult> GetProductPrice(int productId)
//    {
//        var p = await ctx.Products
//            .Include(p => p.Unit)
//            .FirstOrDefaultAsync(p => p.ProductId == productId);

//        if (p is null) return NotFound();

//        return Json(new
//        {
//            p.SellingPrice,
//            p.WholesalePrice,
//            p.CurrentStock,
//            p.Name,
//            Unit = p.Unit?.Name
//        });
//    }

//    public IActionResult ViewSales()
//    {
//        return ViewComponent(nameof(ViewSales));
//    }




//         private async Task<string> GenerateOrderNumberAsync()
//    {
//        var count = await ctx.SaleOrders.CountAsync();
//        return $"INV-{DateTime.UtcNow.Year}-{(count + 1):D4}";
//    }
//}


//using AspNetCoreHero.ToastNotification.Abstractions;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using OSPPOS.Data;
//using OSPPOS.Interfaces;
//using OSPPOS.Models;
//using OSPPOS.Services;
//using OSPPOS.ViewComponents;
//using OSPPOS.ViewModels;
//using System.Security.Claims;

//namespace OSPPOS.Controllers;

//[Authorize]
//public class SaleController(ISalesService sales, XContext ctx, EntityService entityService, INotyfService notyf) : Controller
//{
//    // GET /Sale
//    public async Task<IActionResult> Index(DateTime? from, DateTime? to,
//        PaymentStatus? status, SaleType? type)
//    {
//        from ??= DateTime.Today.AddDays(-30);
//        to ??= DateTime.Today;
//        var orders = await sales.GetOrdersAsync(from, to, status, type);
//        ViewBag.From = from.Value.ToString("yyyy-MM-dd");
//        ViewBag.To = to.Value.ToString("yyyy-MM-dd");
//        ViewBag.Status = status;
//        ViewBag.Type = type;
//        return View(orders);
//    }

//    // POST /Sale/AddSale
//    [HttpPost, ValidateAntiForgeryToken]
//    public async Task<IActionResult> AddSale(AddSaleVM vm)
//    {
//        // Remove empty lines
//        vm.Items.RemoveAll(i => i.ProductId == 0 || i.Quantity == 0);

//        if (vm.Items.Count == 0)
//        {
//            notyf.Error("Please add at least one item.");

//            return ViewComponent(nameof(ViewSales));
//        }

//        // Build the SaleOrder
//        var order = new SaleOrder
//        {
//            OrderNumber = await GenerateOrderNumberAsync(),
//            CustomerId = vm.CustomerId,
//            WalkInCustomerName = vm.WalkInCustomerName,

//            SaleTypeId = vm.SaleTypeId,
//            DueDate = vm.SaleTypeId == SaleType ? vm.DueDate : null,
//            Notes = vm.Notes,
//            Discount = vm.Discount,
//            DiscountPercent = vm.DiscountPercent,
//            OrderDate = DateTime.UtcNow,

//            PaymentStatusId = vm.PaymentStatusId,
//        };

//        // Map line items and reduce stock
//        foreach (var item in vm.Items)
//        {
//            order.Items.Add(new SaleOrderItem
//            {
//                ProductId = item.ProductId,
//                Quantity = item.Quantity,
//                UnitPrice = item.UnitPrice
//            });

//            var product = await ctx.Products.FindAsync(item.ProductId);
//            if (product != null)
//                product.CurrentStock -= item.Quantity;
//        }

//        // Record initial payment if cash was received
//        if (vm.CashReceived > 0)
//        {
//            order.Payments.Add(new Payment
//            {
//                Amount = vm.CashReceived,
//                PaymentMethodId = vm.PaymentMethodId,
//                Reference = vm.PaymentReference,
//            });

//            order.PaymentStatus = vm.CashReceived >= order.TotalAmount
//                ? PaymentStatus.Paid
//                : PaymentStatus.Partial;
//            order.PaymentStatusId = order.PaymentStatusId;
//        }

//        bool result = await entityService.AddEntityAsync(order, User);

//        if (!result)
//        {
//            notyf.Error("Failed to add sale. Please try again.");

//            return ViewComponent(nameof(ViewSales));
//        }

//        notyf.Success($"Sale {order.OrderNumber} added successfully.");
//        return ViewComponent(nameof(ViewSales));
//    }

//    // GET /Sale/Receipt/5
//    public async Task<IActionResult> Receipt(int id)
//    {
//        var order = await sales.GetOrderAsync(id);
//        if (order is null) return NotFound();
//        return View(order);
//    }

//    // GET /Sale/Details/5
//    public async Task<IActionResult> Details(int id)
//    {
//        var order = await sales.GetOrderAsync(id);
//        if (order is null) return NotFound();
//        return View(order);
//    }

//    // GET /Sale/RecordPayment/5
//    public async Task<IActionResult> RecordPayment(int id)
//    {
//        var order = await sales.GetOrderAsync(id);
//        if (order is null) return NotFound();
//        ViewBag.Order = order;
//        return View(new RecordPaymentVm { SaleOrderId = id, Amount = order.AmountDue });
//    }

//    // POST /Sale/RecordPayment
//    [HttpPost, ValidateAntiForgeryToken]
//    public async Task<IActionResult> RecordPayment(RecordPaymentVm vm)
//    {
//        if (!ModelState.IsValid)
//        {
//            ViewBag.Order = await sales.GetOrderAsync(vm.SaleOrderId);
//            return View(vm);
//        }

//        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
//        var (success, error) = await sales.RecordPaymentAsync(vm, userId);

//        if (!success)
//        {
//            ModelState.AddModelError("", error);
//            ViewBag.Order = await sales.GetOrderAsync(vm.SaleOrderId);
//            return View(vm);
//        }

//        notyf.Success("Payment recorded.");
//        return RedirectToAction(nameof(Details), new { id = vm.SaleOrderId });
//    }

//    // AJAX: GET /Sale/GetProductPrice?productId=5
//    [HttpGet]
//    public async Task<IActionResult> GetProductPrice(int productId)
//    {
//        var p = await ctx.Products
//            .Include(p => p.Unit)
//            .FirstOrDefaultAsync(p => p.ProductId == productId);

//        if (p is null) return NotFound();

//        return Json(new
//        {
//            p.SellingPrice,
//            p.WholesalePrice,
//            p.CurrentStock,
//            p.Name,
//            Unit = p.Unit?.Name
//        });
//    }

//    public IActionResult ViewSales()
//    {
//        return ViewComponent(nameof(ViewSales));
//    }

//    private async Task<string> GenerateOrderNumberAsync()
//    {
//        var count = await ctx.SaleOrders.CountAsync();
//        return $"INV-{DateTime.UtcNow.Year}-{(count + 1):D4}";
//    }
//}

using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Data;
using OSPPOS.Interfaces;
using OSPPOS.Models;
using OSPPOS.Services;
using OSPPOS.ViewComponents;
using OSPPOS.ViewModels;
using System.Security.Claims;

namespace OSPPOS.Controllers;

[Authorize]
public class SaleController(ISalesService sales, XContext ctx, EntityService entityService, INotyfService notyf) : Controller
{
    // GET /Sale
    public async Task<IActionResult> Index(DateTime? from, DateTime? to,
        int? statusId, int? saleTypeId)
    {
        from ??= DateTime.Today.AddDays(-30);
        to ??= DateTime.Today;
        var orders = await sales.GetOrdersAsync(from, to, statusId, saleTypeId);
        ViewBag.From = from.Value.ToString("yyyy-MM-dd");
        ViewBag.To = to.Value.ToString("yyyy-MM-dd");
      
       
        return View(orders);
    }

    // POST /Sale/AddSale
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> AddSale(AddSaleVM vm)
    {
        // Remove empty lines
        vm.Items.RemoveAll(i => i.ProductId == 0 || i.Quantity == 0);

        if (vm.Items.Count == 0)
        {
            notyf.Error("Please add at least one item.");

            return ViewComponent(nameof(ViewSales));
        }

        // Look up the chosen sale type so we know whether this is a credit sale
        var saleType = await ctx.SaleTypes.FindAsync(vm.SaleTypeId);

        // The order always starts out at whichever status is flagged as the default;
        // it's updated below if cash was received
        var unpaidStatus = await ctx.PaymentStatuses.FirstOrDefaultAsync(s => s.IsDefault);
        if (unpaidStatus is null)
        {
            notyf.Error("Payment status configuration is missing.");
            return ViewComponent(nameof(ViewSales));
        }

        // Build the SaleOrder
        var order = new SaleOrder
        {
            OrderNumber = await GenerateOrderNumberAsync(),
            CustomerId = vm.CustomerId,
            WalkInCustomerName = vm.WalkInCustomerName,
            SaleTypeId = vm.SaleTypeId,
            DueDate = saleType?.IsCredit == true ? vm.DueDate : null,
            Notes = vm.Notes,
            Discount = vm.Discount,
            DiscountPercent = vm.DiscountPercent,
            OrderDate = DateTime.UtcNow,
            PaymentStatusId = unpaidStatus.PaymentStatusId,
        };

        // Map line items and reduce stock
        foreach (var item in vm.Items)
        {
            order.Items.Add(new SaleOrderItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            });

            var product = await ctx.Products.FindAsync(item.ProductId);
            if (product != null)
                product.CurrentStock -= item.Quantity;
        }

        // Record initial payment if cash was received
        if (vm.CashReceived > 0)
        {
            order.Payments.Add(new Payment
            {
                Amount = vm.CashReceived,
                PaymentMethodId = vm.PaymentMethodId,
                Reference = vm.PaymentReference,
            });

            var isFullyPaid = vm.CashReceived >= order.TotalAmount;
            var paymentStatus = isFullyPaid
                ? await ctx.PaymentStatuses.FirstOrDefaultAsync(s => s.IsFullyPaid)
                : await ctx.PaymentStatuses.FirstOrDefaultAsync(s => s.IsPartiallyPaid);
            if (paymentStatus != null)
                order.PaymentStatusId = paymentStatus.PaymentStatusId;
        }

        bool result = await entityService.AddEntityAsync(order, User);

        if (!result)
        {
            notyf.Error("Failed to add sale. Please try again.");

            return ViewComponent(nameof(ViewSales));
        }

        notyf.Success($"Sale {order.OrderNumber} added successfully.");
        return ViewComponent(nameof(ViewSales));
    }

    // GET /Sale/Receipt/5
    public async Task<IActionResult> Receipt(int id)
    {
        var order = await sales.GetOrderAsync(id);
        if (order is null) return NotFound();
        return View(order);
    }

    // GET /Sale/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var order = await sales.GetOrderAsync(id);
        if (order is null) return NotFound();
        return View(order);
    }

    // GET /Sale/RecordPayment/5
    public async Task<IActionResult> RecordPayment(int id)
    {
        var order = await sales.GetOrderAsync(id);
        if (order is null) return NotFound();
        ViewBag.Order = order;
        return View(new RecordPaymentVm { SaleOrderId = id, Amount = order.AmountDue });
    }

    // POST /Sale/RecordPayment
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> RecordPayment(RecordPaymentVm vm)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Order = await sales.GetOrderAsync(vm.SaleOrderId);
            return View(vm);
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var (success, error) = await sales.RecordPaymentAsync(vm, userId);

        if (!success)
        {
            ModelState.AddModelError("", error);
            ViewBag.Order = await sales.GetOrderAsync(vm.SaleOrderId);
            return View(vm);
        }

        notyf.Success("Payment recorded.");
        return RedirectToAction(nameof(Details), new { id = vm.SaleOrderId });
    }

    // AJAX: GET /Sale/GetProductPrice?productId=5
    [HttpGet]
    public async Task<IActionResult> GetProductPrice(int productId)
    {
        var p = await ctx.Products
            .Include(p => p.Unit)
            .FirstOrDefaultAsync(p => p.ProductId == productId);

        if (p is null) return NotFound();

        return Json(new
        {
            p.SellingPrice,
            p.WholesalePrice,
            p.CurrentStock,
            p.Name,
            Unit = p.Unit?.Name
        });
    }

    public IActionResult ViewSales()
    {
        return ViewComponent(nameof(ViewSales));
    }

    private async Task<string> GenerateOrderNumberAsync()
    {
        var count = await ctx.SaleOrders.CountAsync();
        return $"INV-{DateTime.UtcNow.Year}-{(count + 1):D4}";
    }
}
