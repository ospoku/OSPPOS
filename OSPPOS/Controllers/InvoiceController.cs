using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OSPPOS.DTO.Invoice;
using OSPPOS.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OSPPOS.Controllers
{
    [Authorize]
    public class InvoiceController(
        INotyfService notyf,
        IInvoiceService invoiceService) : Controller
    {
        public async Task<IActionResult> ViewInvoices(ViewInvoicesDTO dto)
        {
            var invoices = await invoiceService.ViewInvoicesAsync(dto);
            return ViewComponent(nameof(ViewInvoices), invoices);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddInvoice(AddInvoiceDTO dto)
        {
            var result = await invoiceService.AddInvoiceAsync(dto, User);

            if (!result.Success)
            {
                notyf.Error("Failed to add invoice.");
                return ViewComponent(nameof(ViewInvoices));
            }

            notyf.Success("Invoice added successfully.");
            return ViewComponent(nameof(ViewInvoices), result);
        }

        // ✅ EDIT (GET)
        public async Task<IActionResult> Edit(int id)
        {
            var invoice = await invoiceService.GetInvoiceForEditAsync(id);

            if (invoice == null)
            {
                notyf.Error("Invoice not found.");
                return NotFound();
            }

            return View(invoice); // usually a DTO/ViewModel
        }

        // ✅ EDIT (POST)
        [HttpPost, ValidateAntiForgeryToken]
  
        public async Task<IActionResult> Edit(UpdateInvoiceDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var result = await invoiceService.UpdateInvoiceAsync(dto, User);

            if (!result)
            {
                notyf.Error("Failed to update invoice.");
                return View(dto);
            }

            notyf.Success("Invoice updated successfully.");
            return RedirectToAction(nameof(ViewInvoices));
        }

        public async Task<IActionResult> GetInvoice(int id)
        {
            var invoice = await invoiceService.GetInvoiceAsync(id);

            if (invoice == null)
            {
                notyf.Error("Invoice not found.");
                return NotFound();
            }

            return View(invoice);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var success = await invoiceService.DeleteInvoiceAsync(id);

            if (!success)
            {
                notyf.Error("Failed to delete invoice.");
                return NotFound();
            }

            notyf.Success("Invoice deleted.");
            return RedirectToAction(nameof(ViewInvoices));
        }
    }
}