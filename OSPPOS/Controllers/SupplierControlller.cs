using AspNetCoreHero.ToastNotification.Abstractions;
using DocumentFormat.OpenXml.ExtendedProperties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Data;
using OSPPOS.Models;
using OSPPOS.Services;
using OSPPOS.ViewComponents;
using OSPPOS.ViewModels;
using System;

namespace OSPPOS.Controllers
{
    [Authorize]
    public class SupplierController(XContext ctx, EntityService entityService, INotyfService notyf) : Controller
    {
      

        public IActionResult ViewSuppliers()
        {
            return ViewComponent(nameof(ViewSuppliers));
        }
      

        [HttpPost, ValidateAntiForgeryToken]
        
        public async Task<IActionResult> AddSupplier(AddSupplierVM addSupplierVM)
        {
       

            var addThisSupplier = new Supplier
            {
                Name = addSupplierVM.Name,
                Phone = addSupplierVM.Phone,
                Email = addSupplierVM.Email,
                Address = addSupplierVM.Address,
                IsActive = addSupplierVM.IsActive
            };


            //ctx.Suppliers.Add(addThisSupplier);
            //await ctx.SaveChangesAsync();

            //return RedirectToAction(nameof(ViewStocks));

            bool result = await entityService.AddEntityAsync(addThisSupplier, User);

            if (!result)
            {
                notyf.Error("Failed to add supplier. Please try again.");
                return ViewComponent(nameof(ViewSuppliers)); // reshow dialog with values intact
            }

            notyf.Success("Supplier added successfully.");
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
