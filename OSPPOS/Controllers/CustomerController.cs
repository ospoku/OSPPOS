using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Data;
using OSPPOS.Models;
using OSPPOS.Services;
using OSPPOS.ViewComponents;
using OSPPOS.ViewModels;
using System;
using System.Diagnostics.Metrics;

namespace OSPPOS.Controllers
{
    
        [Authorize]
        public class CustomerController(XContext ctx, EntityService entityService, INotyfService notyf, IDataProtectionProvider provider) : Controller
        {
        



       

        public IActionResult ViewCustomers() { return ViewComponent(nameof(ViewCustomers)); }

            [HttpPost, ValidateAntiForgeryToken]

        
        public async Task<IActionResult> AddCustomer(AddCustomerVM vm)
        {
            if (!ModelState.IsValid)
            {
                notyf.Error("Enter all fields and try again.");
                return ViewComponent(nameof(ViewCustomers));
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
                return ViewComponent(nameof(ViewCustomers), new { vm }); // reshow dialog with values intact
            }

            notyf.Success("Customer added successfully.");
            return RedirectToAction(nameof(ViewCustomers));
        }
        [HttpGet]
        public IActionResult EditLetter(Guid Id) => ViewComponent(nameof(EditLetter), Id);

        [HttpPost]
        public async Task<IActionResult> EditLetterAsync(Guid id, Customer customer)
        {
            try
            {
                var customerToUpdate = await ctx.Customers.FirstOrDefaultAsync(a => a.PublicId == customer.PublicId);
                if (customerToUpdate == null)
                {
                    return NotFound();
                }

                customerToUpdate.Address = customer.Address;
                customerToUpdate.TaxNumber = customer.TaxNumber;
                
                ctx.Customers.Attach(customerToUpdate);
                ctx.Customers.Entry(customerToUpdate).State = EntityState.Modified;

                if (await ctx.SaveChangesAsync() > 0)
                {
                    notyf.Success("Record successfully updated.");
                    return RedirectToAction("ViewLetters");
                }
                else
                {
                    notyf.Error("Document saving failed.");
                    return ViewComponent(nameof(ViewCustomers));
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { message = "An error occurred while updating the document: " + ex.Message });
            }
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

