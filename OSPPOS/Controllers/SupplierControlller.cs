using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Data;
using OSPPOS.Models;
using System;

namespace OSPPOS.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class SuppliersController(XContext db) : Controller
    {
        private readonly XContext ctx = db;

        public async Task<IActionResult> Index() =>
            View(await ctx.Suppliers.OrderBy(s => s.Name).ToListAsync());

        public IActionResult Create() => View(new Supplier());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Supplier model)
        {
            if (!ModelState.IsValid) return View(model);
            ctx.Suppliers.Add(model);
            await ctx.SaveChangesAsync();
            TempData["Success"] = "Supplier added.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var s = await ctx.Suppliers.FindAsync(id);
            if (s is null) return NotFound();
            return View(s);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Supplier model)
        {
            if (!ModelState.IsValid) return View(model);
            ctx.Suppliers.Update(model);
            await ctx.SaveChangesAsync();
            TempData["Success"] = "Supplier updated.";
            return RedirectToAction(nameof(Index));
        }
    }
}
