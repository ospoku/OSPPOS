using AspNetCoreHero.ToastNotification.Abstractions;
using DMX.Data;

using DMX.Models;
using DMX.Services;
using DMX.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DMX.Controllers
{
    public class DeceasedController(XContext dContext, UserManager<AppUser> userManager, INotyfService notyfService, EmailService emailService,
          EntityService entityService, AssignmentService assignmentService) : Controller
    {
        public readonly UserManager<AppUser> usm = userManager;
        public readonly XContext dcx = dContext;
        private readonly INotyfService notyf = notyfService;
        public readonly EntityService entityServ = entityService;
        public readonly EmailService email = emailService;
        public readonly AssignmentService assignmentServ = assignmentService;

        [HttpGet]
        public IActionResult EditDeceased(Guid Id)
        {
            return ViewComponent(nameof(EditDeceased), Id);
        }

        public IActionResult ViewDeceaseds()
        {
            return ViewComponent(nameof(ViewDeceaseds));
        }

        public async Task<string> GetUserEmailAsync(string userId)
        {
            var user = await usm.FindByIdAsync(userId);
            return user?.Email;
        }

        [HttpGet]
        public IActionResult AddDeceased() => ViewComponent(nameof(AddDeceased));

        [HttpPost]
        public async Task<IActionResult> AddDeceased(AddDeceasedVM addDeceasedVM)
        {


            if (addDeceasedVM.SelectedUsers?.Any() != true)
            {
                notyf.Error("You must select at least one user for assignment.", 5);
                return RedirectToAction(nameof(ViewDeceaseds));
            }
            if (dcx.FeeStructures?.Any() != true)
            {
                notyf.Error("You must set Fee structures", 5);
                return RedirectToAction(nameof(ViewDeceaseds));
            }

            try
            {
                var existingPatient = await dcx.Deceased.FirstOrDefaultAsync(p =>
                    p.Name.ToLower() == addDeceasedVM.DeceasedName.ToLower() &&
                    p.Depositor.ToLower() == addDeceasedVM.Depositor.ToLower());

                if (existingPatient != null)
                {
                    notyf.Error("This record already exists.");
                    return RedirectToAction(nameof(ViewDeceaseds));
                }

                Deceased deceased = new()
                {
                    TagNo=addDeceasedVM.TagNo,
                    FolderNo=addDeceasedVM.FolderNo,
                    WardInCharge=addDeceasedVM.WardInCharge,
                    DeceasedTypeId=addDeceasedVM.DeceasedTypeId,
                    Description=addDeceasedVM.Description,
                    Depositor=addDeceasedVM.Depositor,
                    DepositorAddress=addDeceasedVM.DepositorAddress,
                    Diagnoses=addDeceasedVM.Diagnoses,
                    Name=addDeceasedVM.DeceasedName
                };

                var patientAdded = await entityServ.AddEntityAsync(deceased, User);
                if (!patientAdded)
                {
                    notyf.Error("Failed to add patient.", 5);
                    return RedirectToAction("Error", "Home", new { message = "Patient creation failed." });
                }

                if (addDeceasedVM.SelectedUsers?.Any() == true)
                {
                    foreach (var userId in addDeceasedVM.SelectedUsers)
                    {
                        var assignment = new DeceasedAssignment
                        {
                            DeceasedId = deceased.DeceasedId,
                            UserId = userId,
                        };

                        var assignmentAdded = await entityServ.AddEntityAsync(assignment, User);
                        if (!assignmentAdded)
                        {
                            notyf.Error("Failed to assign user.", 5);
                            return RedirectToAction("Error", "Home", new { message = "User assignment failed." });
                        }
                    }
                    notyf.Success("Patient and assignments successfully processed.", 5);
                }
                else
                {
                    notyf.Success("Patient created successfully, no users assigned.", 5);
                }

                return RedirectToAction(nameof(ViewDeceaseds));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                notyf.Error("An unexpected error occurred.", 5);
                return RedirectToAction("Error", "Home", new { message = "An unexpected error occurred." });
            }
        }

        [HttpGet]
        public IActionResult PrintPatient(Guid Id)
        {
            return ViewComponent(nameof(PrintPatient), Id);
        }

        [HttpGet]
        public IActionResult CommentDeceased(Guid Id)
        {
            return ViewComponent(nameof(CommentDeceased), Id);
        }

        [HttpGet]
        public IActionResult ViewInvoice(string Id)
        {
            return ViewComponent(nameof(ViewInvoice), Id);
        }
    }
}
