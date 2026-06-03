using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DMX.Data;
using DMX.ViewModels;
using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using DMX.Models;
using Microsoft.AspNetCore.Identity;
using DMX.ViewComponents;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using DMX.Helpers;
using DMX.Services;
using Microsoft.AspNetCore.Components.Web;


namespace DMX.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class SettingsController(XContext context, INotyfService notyfService, UserManager<AppUser> userManager, EntityService entityService) : Controller
    {
        public readonly XContext dcx = context;
        public readonly INotyfService notyf = notyfService;
        public readonly UserManager<AppUser> usm = userManager;
        public readonly EntityService entityServ = entityService;

   
        public IActionResult SystemSetup()
        {
            return ViewComponent(nameof(SystemSetup));
        }

        [HttpPost]
        public async Task<IActionResult> AddTravelTypeAsync(AddTravelTypeVM addTravelTypeVM)
        {
            TravelType travelType = new()
            {
                Name = addTravelTypeVM.Name,
                Code = addTravelTypeVM.Code,
                Description = addTravelTypeVM.Description,
            };

            if (await entityServ.AddEntityAsync(travelType, User))
            {
                return RedirectToAction("SystemSetup");
            }
            return RedirectToAction("SystemSetup");

        }
        [HttpPost]
        public async Task<IActionResult> AddFeeStructureAsync(AddFeeStructureVM addFeeStructureVM)
        {

            string RefN = "F" + Guid.NewGuid().ToString("N").Substring(0, 5);
            var user = await usm.GetUserAsync(User);

            FeeStructure addThisStructure = new()
            {
                Name = addFeeStructureVM.Name,
                DeceasedTypeId = addFeeStructureVM.DeceasedTypeId,
                MinDays = addFeeStructureVM.Min,
                MaxDays = addFeeStructureVM.Max,
                Fee = addFeeStructureVM.Fee,
                

            };
            // Call the service method, which returns a bool
            bool result = await entityServ.AddEntityAsync(addThisStructure, User);

            // Based on the result, redirect or return the appropriate response
            if (result)
            {
                notyf.Success("Record successfully saved", 5);
                // Success: Redirect to SystemSetup
                return RedirectToAction(nameof(SystemSetup));
            }
            else
            {
                notyf.Error("Error, Record could not be saved!!!", 5);
                // Failure: Return an error view or handle as needed
                return RedirectToAction(nameof(SystemSetup)); // You can return an error page if preferred
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddDeceasedTypeAsync(AddDeceasedTypeVM addDeceasedTypeVM)
        {

            string RefN = "D" + Guid.NewGuid().ToString().Substring(0, 5);
            var user = await usm.GetUserAsync(User);

            DeceasedType addThisDeceasedType = new()
            {
                Name = addDeceasedTypeVM.Name,
                Code = addDeceasedTypeVM.Code,
                Description = addDeceasedTypeVM.Description
            };
            // Call the service method, which returns a bool
            bool result = await entityServ.AddEntityAsync(addThisDeceasedType, User);

            // Based on the result, redirect or return the appropriate response
            if (result)
            {
                // Success: Redirect to SystemSetup
                return RedirectToAction(nameof(SystemSetup));
            }
            else
            {
                // Failure: Return an error view or handle as needed
                return RedirectToAction(nameof(SystemSetup)); // You can return an error page if preferred
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartmentAsync(AddDepartmentVM addDepartmentVM)
        {
            var user = await usm.GetUserAsync(User);

            string RefN = "D" + Guid.NewGuid().ToString("N").Substring(0, 5);

            Department addThisDepartment = new()
            {
                Name = addDepartmentVM.Name,

                Code = addDepartmentVM.Code,
                Description = addDepartmentVM.Description
            };
            // Call the service method, which returns a bool
            bool result = await entityServ.AddEntityAsync(addThisDepartment, User);

            // Based on the result, redirect or return the appropriate response
            if (result)
            {
                // Success: Redirect to SystemSetup
                return RedirectToAction(nameof(SystemSetup));
            }
            else
            {
                // Failure: Return an error view or handle as needed
                return RedirectToAction(nameof(SystemSetup)); // You can return an error page if preferred
            }
        }

        [HttpGet]
        public IActionResult AddPerDiem()
        {
            return ViewComponent(nameof(AddPerDiem));
        }
        [HttpPost]
        public async Task<IActionResult> AddCashLimit(EditLimitVM limitVM, CashLimit cashLimit)
        {

            // Assuming you save the limit in a database or some other store

            CashLimit addThisLimit = new()
            {
                Amount = cashLimit.Amount,
            };

            bool result = await entityServ.AddEntityAsync(addThisLimit, User);

            // Based on the result, redirect or return the appropriate response
            if (result)
            {
                // Success: Redirect to SystemSetup
                return RedirectToAction(nameof(SystemSetup));
            }
            else
            {
                // Failure: Return an error view or handle as needed
                return RedirectToAction(nameof(SystemSetup)); // You can return an error page if preferred
            }

        }

        [HttpPost]
        public async Task<IActionResult> EditCashLimit(string id, EditLimitVM editLimitVM)
        {
            

            try
            {
                var unprotectedId = (id);
                var limitToUpdate = await dcx.CashLimits.FirstOrDefaultAsync(m => m.CashLimitId == unprotectedId);
                if (limitToUpdate == null)
                {
                    notyf.Error("Limit not found.", 5);
                    return RedirectToAction(nameof(SystemSetup));
                }

                limitToUpdate.Amount = editLimitVM.Amount;
               

                bool isEdited = await entityServ.EditEntityAsync(limitToUpdate, User);
                if (!isEdited)
                {
                    notyf.Error("Failed to update memo. Please try again.", 5);
                    return RedirectToAction("ViewMemos");
                }
else
                {
                    notyf.Success("Record successfully updated", 5);
                }

                return RedirectToAction("ViewMemos");
            }
            catch (Exception ex)
            {
               notyf.Error("An unexpected error occurred. Please try again.", 5);
                Console.WriteLine($"Error updating Memo: {ex.Message}");
                return RedirectToAction("Error", "Home", new { message = "An error occurred while processing the memo." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddTransportModeAsync(AddTransportModeVM addTransportVM)
        {
            var user = await usm.GetUserAsync(User);

            string RefN = "T" + Guid.NewGuid().ToString("N").Substring(0, 5);

            TransportType addThisTransport = new()
            {
                Name = addTransportVM.Name,

                Code = addTransportVM.Code,
                Description = addTransportVM.Description,
            };
            // Call the service method, which returns a bool
            bool result = await entityServ.AddEntityAsync(addThisTransport, User);

            // Based on the result, redirect or return the appropriate response
            if (result)
            {
                // Success: Redirect to SystemSetup
                return RedirectToAction(nameof(SystemSetup));
            }
            else
            {
                // Failure: Return an error view or handle as needed
                return RedirectToAction(nameof(SystemSetup)); // You can return an error page if preferred
            }

        }


        [HttpPost]
        public async Task<IActionResult> EditPerDiemAsync(EditPerdiemVM editPerdiemVM, string Id)
        {
            try
            {
                var unprotectedId = (Id);
                var perdiemToUpdate = await dcx.PerDiems.FirstOrDefaultAsync(m => m.UserId == unprotectedId);

                if (perdiemToUpdate == null)
                {
                    // User not found, create a new PerDiem record
                    var newPerDiem = new PerDiem
                    {
                        UserId = unprotectedId,
                        Amount = editPerdiemVM.Amount
                    };

                    bool isAdded = await entityServ.AddEntityAsync(newPerDiem, User);
                    if (!isAdded)
                    {
                        notyf.Error("Failed to add perdiem. Please try again.", 5);
                        return RedirectToAction(nameof(SystemSetup));
                    }
                    else
                    {
                        notyf.Success("Record successfully added", 5);
                    }
                }
                else
                {
                    // User found, update the existing PerDiem record
                    perdiemToUpdate.Amount = editPerdiemVM.Amount;

                    bool isEdited = await entityServ.EditEntityAsync(perdiemToUpdate, User);
                    if (!isEdited)
                    {
                        notyf.Error("Failed to update perdiem. Please try again.", 5);
                        return RedirectToAction(nameof(SystemSetup));
                    }
                    else
                    {
                        notyf.Success("Record successfully updated", 5);
                    }
                }

                return RedirectToAction(nameof(SystemSetup));
            }
            catch (Exception ex)
            {
                notyf.Error("An unexpected error occurred. Please try again.", 5);
                Console.WriteLine($"Error updating Perdiem: {ex.Message}");
                return RedirectToAction("Error", "Home", new { message = "An error occurred while processing the perdiem." });
            }
        }
            [HttpGet]
        public IActionResult EditPerDiem(string Id)
        {

            return ViewComponent(nameof(EditPerDiem),Id);
        }
    }


}







