using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Data;
using OSPPOS.Models;
using System;

namespace OSPPOS.Controllers
{
    
        [Authorize(Roles = "Admin,Manager")]
        public class CustomersController(XContext db) : Controller
        {
            private readonly XContext ctx = db;

        public async Task<IActionResult> Index() =>
                View(await ctx.Customers
                    .Include(c => c.SaleOrders).ThenInclude(o => o.Payments)
                    .OrderBy(c => c.Name).ToListAsync());

            public IActionResult Create() => View(new Customer());

            [HttpPost, ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(Customer model)
            {
                if (!ModelState.IsValid) return View(model);
                ctx.Customers.Add(model);
                await ctx.SaveChangesAsync();
                TempData["Success"] = "Customer created.";
                return RedirectToAction(nameof(Index));
            }

            public async Task<IActionResult> Edit(int id)
            {
                var c = await ctx.Customers.FindAsync(id);
                if (c is null) return NotFound();
                return View(c);
            }

            [HttpPost, ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(Customer model)
            {
                if (!ModelState.IsValid) return View(model);
                ctx.Customers.Update(model);
                await ctx.SaveChangesAsync();
                TempData["Success"] = "Customer updated.";
                return RedirectToAction(nameof(Index));
            }

            public async Task<IActionResult> Statement(int id)
            {
                var c = await ctx.Customers
                    .Include(x => x.SaleOrders).ThenInclude(o => o.Items).ThenInclude(i => i.Product)
                    .Include(x => x.SaleOrders).ThenInclude(o => o.Payments)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (c is null) return NotFound();
                return View(c);
            }
        }
    }

