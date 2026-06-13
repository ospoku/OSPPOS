using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Data;
using OSPPOS.Models;
using OSPPOS.ViewModels;
using System;

namespace OSPPOS.Controllers
{
    [Authorize]
    public class SupplierController(XContext ctx) : Controller
    {
      

        public IActionResult ViewSuppliers()
        {
            return ViewComponent(nameof(ViewSuppliers));
        }
        public async Task<IActionResult> Index() =>
            View(await ctx.Suppliers.OrderBy(s => s.Name).ToListAsync());

        public IActionResult Create() => View(new Supplier());

        [HttpPost, ValidateAntiForgeryToken]
        
        public async Task<IActionResult> AddSupplier(AddSupplierVM vm)
        {
       

            var addThisSupplier = new Supplier
            {
                Name = vm.Name,
                Phone = vm.Phone,
                Email = vm.Email,
                Address = vm.Address,
                IsActive = vm.IsActive
            };

            ctx.Suppliers.Add(addThisSupplier);
            await ctx.SaveChangesAsync();

            return RedirectToAction(nameof(ViewSuppliers));
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

        public IActionResult AddSupplier()
        {
            return ViewComponent(nameof(AddSupplier));
        }
    }
}
