using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using DocumentFormat.OpenXml.ExtendedProperties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class ProductController(XContext ctx,EntityService entityService, INotyfService notyf) : Controller
    {
        

        public IActionResult ViewProducts()
        {

            return ViewComponent(nameof(ViewProducts));

        }

        public async Task<IActionResult> AddProduct()
        {
            await PopulateDropDownsAsync();
            return View(new Product());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(AddProductVM addProductVM)
        {
            //if (!ModelState.IsValid)
            //{
            //    return ViewComponent(nameof(AddProduct), new { addProductVM });
            //}

            Product addThisProduct = new()
            {
                Name = addProductVM.Name,
                Description = addProductVM.Description,
                CategoryId=addProductVM.CategoryId,
                SupplierId=addProductVM.SupplierId,
                CostPrice = addProductVM.CostPrice,
                IsActive=addProductVM.IsActive,
                SellingPrice = addProductVM.SellingPrice,
                SKU=addProductVM.SKU,
                ReorderLevel=addProductVM.ReorderLevel,
                UnitId=addProductVM.UnitId,
                WholesalePrice=addProductVM.WholesalePrice,
                CurrentStock=addProductVM.CurrentStock,

            };

            bool result = await entityService.AddEntityAsync(addThisProduct, User);

            if (!result)
            {
                notyf.Error("Failed to add customer. Please try again.");
                return ViewComponent(nameof(AddCustomer), new {addProductVM }); // reshow dialog with values intact
            }

            notyf.Success("Customer added successfully.");
            return RedirectToAction(nameof(ViewCustomers));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var p = await ctx.Products.FindAsync(id);
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

            ctx.Products.Update(model);
            await ctx.SaveChangesAsync();

            TempData["Success"] = "Product updated.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleActive(int id)
        {
            var p = await ctx.Products.FindAsync(id);
            if (p is null) return NotFound();

            p.IsActive = !p.IsActive;
            await ctx   .SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateDropDownsAsync()
        {
            ViewBag.Categories = new SelectList(
                await ctx.Categories.Where(c => c.IsActive).ToListAsync(), "Id", "Name");

            ViewBag.Suppliers = new SelectList(
                await ctx.Suppliers.Where(s => s.IsActive).ToListAsync(), "Id", "Name");
        }

        private async Task<string> GenerateSKUAsync(int categoryId)
        {
            var cat = await ctx.Categories.FindAsync(categoryId);
            var prefix = cat?.Name[..Math.Min(3, cat.Name.Length)].ToUpper() ?? "PRD";
            var count = await ctx.Products.CountAsync(p => p.CategoryId == categoryId);

            return $"{prefix}-{count + 1:D4}";
        }
    }
}

