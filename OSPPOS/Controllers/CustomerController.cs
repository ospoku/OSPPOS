using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OSPPOS.DTO.Customer;
using OSPPOS.DTO.Product;
using OSPPOS.Interfaces;
using OSPPOS.Models;
using OSPPOS.Services;
using OSPPOS.ViewComponents;
using OSPPOS.ViewModels;

namespace OSPPOS.Controllers
{
    [Authorize]
    public class CustomerController(ICustomerService customerService, INotyfService notyf) : Controller
    {


        public async Task< IActionResult> ViewCustomers(ViewCustomersDTO viewCustomersDTO)
        {
            var customers = await customerService.ViewCustomersAsync(viewCustomersDTO);

      
            if (customers == null || customers.Count == 0)
            {
                notyf.Error("Failed to display customers. Please try again.");
                return ViewComponent(nameof(ViewCustomers));
            }



            return ViewComponent(nameof(ViewCustomers), customers);

        }

        

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCustomer(AddCustomerVM vm)
        {
            if (!ModelState.IsValid)
            {
                notyf.Error("Enter all fields and try again.");
                return ViewComponent(nameof(ViewCustomers));
            }

            var result = await customerService.AddCustomerAsync(vm, User);

            if (!result)
            {
                notyf.Error("Failed to add customer.");
                return ViewComponent(nameof(ViewCustomers), new { vm });
            }

            notyf.Success("Customer added successfully.");
            return RedirectToAction(nameof(ViewCustomers));
        }

        [HttpGet]
        public IActionResult EditCustomer(Guid id)
            => ViewComponent(nameof(EditCustomer), id);

        [HttpPost]
        public async Task<IActionResult> EditCustomerAsync(Customer customer)
        {
            var result = await customerService.UpdateCustomerAsync(customer);

            if (!result)
            {
                notyf.Error("Update failed.");
                return ViewComponent(nameof(ViewCustomers));
            }

            notyf.Success("Record updated.");
            return RedirectToAction(nameof(ViewCustomers));
        }

        public async Task<IActionResult> Statement(int id)
        {
            var customer = await customerService.GetCustomerStatementAsync(id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }
    }
}