using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OSPPOS.Data;
using OSPPOS.DTO.Patient;
using OSPPOS.DTO.Product;
using OSPPOS.Interfaces;
using OSPPOS.Models;
using OSPPOS.ViewModels;

namespace OSPPOS.Controllers
{
    [Authorize]
    public class PatientController(XContext ctx, INotyfService notyf, IPatientService PatientService) : Controller
    {


        public async Task<IActionResult> ViewPatients(ViewPatientsDTO viewPatientsDTO)
        {

            var results = await PatientService.ViewPatientsAsync(viewPatientsDTO);
            if (results == null||results.Count == 0)
            {
                notyf.Error("Failed to display Patients. Please try again.");
                return ViewComponent(nameof(ViewPatients));
            }

   

            return ViewComponent(nameof(ViewPatients),results);
            
        }

        public async Task<IActionResult> AddPatient()
        {
            await PopulateDropDownsAsync();
            return View(new Patient());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPatient(AddPatientVM addPatientVM)
        {


           AddPatientDTO addThisPatient = new()
            {
                Name = addPatientVM.Name,
                Description = addPatientVM.Description,
                CategoryId = addPatientVM.CategoryId,
                SupplierId = addPatientVM.SupplierId,
                CostPrice = addPatientVM.CostPrice,
                IsActive = addPatientVM.IsActive,
                SellingPrice = addPatientVM.SellingPrice,
                SKU = addPatientVM.SKU,
                ReorderLevel = addPatientVM.ReorderLevel,
                UnitId = addPatientVM.UnitId,
                WholesalePrice = addPatientVM.WholesalePrice,
                CurrentStock = addPatientVM.CurrentStock,

            };


        var result = await PatientService.AddPatientAsync(addThisPatient, User);

            if (!result.Success)
            {
                notyf.Error("Failed to add customer. Please try again.");
                return ViewComponent(nameof(ViewPatients)); // reshow dialog with values intact
            }
            else
            {
                notyf.Success("Customer added successfully.");
                return ViewComponent(nameof(ViewPatients));
             }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var p = await ctx.Patients.FindAsync(id);
            if (p is null) return NotFound();

            await PopulateDropDownsAsync();
            return View(p);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Patient model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropDownsAsync();
                return View(model);
            }

            ctx.Patients.Update(model);
            await ctx.SaveChangesAsync();

            TempData["Success"] = "Patient updated.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleActive(int id)
        {
            var p = await ctx.Patients.FindAsync(id);
            if (p is null) return NotFound();

           
            await ctx   .SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateDropDownsAsync()
        {
      

         
        }

    
    }
}

