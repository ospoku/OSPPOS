using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Data;
using OSPPOS.Models;
using OSPPOS.Services;
using OSPPOS.ViewModels;
using System;

namespace OSPPOS.Controllers
{
    
        [Authorize]
        public class CustomerController(XContext ctx, EntityService entityService, INotyfService notyf) : Controller
        {
           

        public async Task<IActionResult> Index() =>
                View(await ctx.Customers
                    .Include(c => c.SaleOrders).ThenInclude(o => o.Payments)
                    .OrderBy(c => c.Name).ToListAsync());

            public IActionResult AddCustomer() => View(new Customer());
        public IActionResult ViewCustomers() { return ViewComponent(nameof(ViewCustomers)); }

            [HttpPost, ValidateAntiForgeryToken]

        
        public async Task<IActionResult> AddCustomer(AddCustomerVM vm)
        {
            if (!ModelState.IsValid)
            {
                return ViewComponent(nameof(AddCustomer), new { vm });
            }

            var addThisCustomer = new Customer
            {
                Name = vm.Name,
                Email = vm.Email,
                Phone = vm.Phone,
                Address = vm.Address,
                TaxNumber = vm.TaxNumber,
                CreditLimit = vm.AllowCredit ? vm.CreditLimit : 0,
                AllowCredit = vm.AllowCredit,
                IsActive = vm.IsActive
            };

            bool result = await entityService.AddEntityAsync(addThisCustomer, User);

            if (!result)
            {
                notyf.Error("Failed to add customer. Please try again.");
                return ViewComponent(nameof(AddCustomer), new { vm }); // reshow dialog with values intact
            }

            notyf.Success("Customer added successfully.");
            return RedirectToAction(nameof(ViewCustomers));
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

