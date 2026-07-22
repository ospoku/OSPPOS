using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OSPPOS.Models;
using OSPPOS.Services;
using OSPPOS.ViewModels;

namespace OSPPOS.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly CustomerService _customerService;
        private readonly INotyfService _notyf;

        public CustomerController(CustomerService customerService, INotyfService notyf)
        {
            _customerService = customerService;
            _notyf = notyf;
        }

        public IActionResult ViewCustomers()
            => ViewComponent(nameof(ViewCustomers));

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCustomer(AddCustomerVM vm)
        {
            if (!ModelState.IsValid)
            {
                _notyf.Error("Enter all fields and try again.");
                return ViewComponent(nameof(ViewCustomers));
            }

            var result = await _customerService.AddCustomerAsync(vm, User);

            if (!result)
            {
                _notyf.Error("Failed to add customer.");
                return ViewComponent(nameof(ViewCustomers), new { vm });
            }

            _notyf.Success("Customer added successfully.");
            return RedirectToAction(nameof(ViewCustomers));
        }

        [HttpGet]
        public IActionResult EditCustomer(Guid id)
            => ViewComponent(nameof(EditCustomer), id);

        [HttpPost]
        public async Task<IActionResult> EditCustomerAsync(Customer customer)
        {
            var result = await _customerService.UpdateCustomerAsync(customer);

            if (!result)
            {
                _notyf.Error("Update failed.");
                return ViewComponent(nameof(ViewCustomers));
            }

            _notyf.Success("Record updated.");
            return RedirectToAction(nameof(ViewCustomers));
        }

        public async Task<IActionResult> Statement(int id)
        {
            var customer = await _customerService.GetCustomerStatementAsync(id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }
    }
}