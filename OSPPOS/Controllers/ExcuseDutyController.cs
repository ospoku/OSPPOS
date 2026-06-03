using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using DMX.Data;

using DMX.Models;
using DMX.Services;
using DMX.ViewComponents;
using DMX.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DMX.Controllers
{
    [Authorize]
    public class ExcuseDutyController : Controller
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<AppUser> _userManager;
        private readonly XContext _context;
        private readonly INotyfService _notyfService;
        private readonly EntityService _entityService;
        private readonly AssignmentService _assignmentService;
        public readonly IDataProtector protector;

        public ExcuseDutyController(
            XContext context,
            UserManager<AppUser> userManager,
            INotyfService notyfService,
            EntityService entityService,
            IAuthorizationService authorizationService,
            AssignmentService assignmentService, IDataProtectionProvider protectionProvider)
        {
            _authorizationService = authorizationService;
            _userManager = userManager;
            _context = context;
            _notyfService = notyfService;
            _entityService = entityService;
            _assignmentService = assignmentService;
            protector = protectionProvider.CreateProtector("IdProtector");
        }

        [HttpGet]
        public IActionResult ViewExcuseDuties()
        {
            return ViewComponent(nameof(ViewExcuseDuties));
        }

        [HttpGet]
        public IActionResult DetailExcuseDuty(string id)
        {
            return ViewComponent(nameof(DetailExcuseDuty), id);
        }

        [HttpPost]
        public async Task<IActionResult> CommentExcuseDuty(string Id, ExcuseDutyCommentVM commentVm)
        {
            
            var protectedId = protector.Protect(Id);
   if(string.IsNullOrEmpty(commentVm.NewComment))
            {
                _notyfService.Error("Comment cannot be empty.", 5);
                return RedirectToAction("ViewExcuseDuties");
            }
   if(!Guid.TryParse(protectedId, out Guid excuseDutyGuid))
            {
                _notyfService.Error("Invalid Excuse Duty ID format.", 5);
                return RedirectToAction("ViewExcuseDuties");
            }

            try
            {
                var dutyToComment = await _context.ExcuseDuties.FirstOrDefaultAsync(a => a.PublicId == excuseDutyGuid);
                if (dutyToComment == null)
                {
                    _notyfService.Error("The recoord could not be found");
                }

                var newComment = new ExcuseDutyComment
                {
                    ExcuseDutyId = dutyToComment.Id,
                    Message = commentVm.NewComment,
                    UserId = (await _userManager.GetUserAsync(User)).Id
                };

                bool result = await _entityService.AddEntityAsync(newComment, User);
                if (result)
                {
                    _notyfService.Success("Comment successfully saved", 5);
                }
                else
                {
                    _notyfService.Error("Comment could not be saved!!!", 5);
                }

                return RedirectToAction("ViewExcuseDuties");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { message = "An error occurred while processing the request.", ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditExcuseDutyAsync(string id)
        {
            
            var protectedId = protector.Unprotect(id);
            if(string.IsNullOrEmpty(protectedId))
            {
                return BadRequest();
            }
            if(!Guid.TryParse(protectedId, out Guid excuseDutyGuid))
            {
                return BadRequest();
            }
            var excuseDuty = await _context.ExcuseDuties.FirstOrDefaultAsync(x => x.PublicId == excuseDutyGuid);
            if (excuseDuty == null)
            {
                return NotFound();
            }

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, excuseDuty, "ExcuseDutyOwnerPolicy");
            if (!authorizationResult.Succeeded)
            {
                _notyfService.Error("You do not have access to this resource!", 5);
                return Json(new { success = false, message = "You do not have access to this resource!" });
            }
            return ViewComponent("EditExcuseDuty", id);
        }

        [HttpPost]
        public async Task<IActionResult> AddExcuseDuty(AddExcuseDutyVM addExcuseDutyVm)
        {
            if (addExcuseDutyVm.SelectedUsers == null || !addExcuseDutyVm.SelectedUsers.Any())
            {
                _notyfService.Error("You must select at least one user for assignment.", 5);
                return RedirectToAction(nameof(ViewExcuseDuties));
            }

            try
            {
                var existingRecord = await _context.ExcuseDuties.FirstOrDefaultAsync(e =>
                    e.ExcuseDays == addExcuseDutyVm.ExcuseDays &&
                    e.DateofDischarge == addExcuseDutyVm.DateofDischarge &&
                    e.Diagnosis == addExcuseDutyVm.Diagnosis &&
                    e.PatientName == addExcuseDutyVm.PatientName &&
                    e.PatientId == addExcuseDutyVm.PatientId);

                if (existingRecord != null)
                {
                    _notyfService.Error("This record already exists.");
                    return RedirectToAction(nameof(ViewExcuseDuties));
                }

                var newExcuseDuty = new ExcuseDuty
                {
                    DateofDischarge = addExcuseDutyVm.DateofDischarge,
                    ExcuseDays = addExcuseDutyVm.ExcuseDays,
                    PatientName = addExcuseDutyVm.PatientName,
                    PatientId = addExcuseDutyVm.PatientId,
                    Diagnosis = addExcuseDutyVm.Diagnosis
                };

                bool result = await _entityService.AddEntityAsync(newExcuseDuty, User);
                if (!result)
                {
                    _notyfService.Error("Excuse Duty creation failed.", 5);
                    return RedirectToAction("ErrorPage", new { message = "Failed to add the Excuse Duty. Please try again." });
                }

                foreach (var user in addExcuseDutyVm.SelectedUsers)
                {
                    var assignment = new ExcuseDutyAssignment
                    {
                        ExcuseDutyId = newExcuseDuty.Id,
                        UserId = user
                    };

                    bool assignResult = await _entityService.AddEntityAsync(assignment, User);
                    if (!assignResult)
                    {
                        _notyfService.Error("Error, record could not be saved.", 5);
                        return RedirectToAction("ViewExcuseDuties");
                    }
                }

                _notyfService.Success("Record successfully saved", 5);
                return RedirectToAction("ViewExcuseDuties");
            }
            catch (Exception ex)
            {
                _notyfService.Error("An error occurred: " + ex.Message, 5);
                return RedirectToAction("ErrorPage", new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditExcuseDutyAsync(string id, EditExcuseDutyVM editExcuseDutyVm)
        {
            if (editExcuseDutyVm.SelectedUsers == null || !editExcuseDutyVm.SelectedUsers.Any())
            {
                _notyfService.Error("You must select at least one user for assignment.", 5);
                return RedirectToAction("ViewExcuseDuties");
            }

            try
            {
                
                var unprotectedId = protector.Unprotect(id);
                if(!Guid.TryParse(unprotectedId, out Guid dutyGuid))
                {

                    return View("Error", "Invalid Excuse Duty Id format");

                }
                var excuseDutyToUpdate = _context.ExcuseDuties.FirstOrDefault(a => a.PublicId == dutyGuid);
                if (excuseDutyToUpdate == null)
                {
                    return NotFound();
                }

                excuseDutyToUpdate.DateofDischarge = editExcuseDutyVm.DateofDischarge;
                excuseDutyToUpdate.ExcuseDays = editExcuseDutyVm.ExcuseDays;
                excuseDutyToUpdate.Diagnosis = editExcuseDutyVm.Diagnosis;
                excuseDutyToUpdate.PatientName = editExcuseDutyVm.Name;
                excuseDutyToUpdate.PatientId = editExcuseDutyVm.PatientId;

                bool isEdited = await _entityService.EditEntityAsync(excuseDutyToUpdate, User);
                if (!isEdited)
                {
                    _notyfService.Error("Failed to update memo. Please try again.", 5);
                    return RedirectToAction("ViewMemos");
                }

                var existingAssignments = _context.ExcuseDutyAssignments.Where(x => x.PublicId == dutyGuid);
                _context.ExcuseDutyAssignments.RemoveRange(existingAssignments);

                foreach (var userId in editExcuseDutyVm.SelectedUsers)
                {
                    bool reassign = await _assignmentService.AssignUsers(new ExcuseDutyAssignment { Id = excuseDutyToUpdate.Id, UserId = userId }, User);
                    if (!reassign)
                    {
                        _notyfService.Error("Failed to reassign users.", 5);
                        return RedirectToAction("ViewMemos");
                    }
                }

                _notyfService.Success("Record successfully updated", 5);
                return RedirectToAction("ViewMemos");
            }
            catch (Exception ex)
            {
                _notyfService.Error("An error occurred: " + ex.Message, 5);
                return RedirectToAction("Error", "Home", new { message = "An error occurred while processing the memo." });
            }
        }

        [HttpGet]
        public IActionResult AddExcuseDuty()
        {
            return ViewComponent(nameof(AddExcuseDuty));
        }

        [HttpGet]
        public IActionResult CommentExcuseDuty(string Id) => ViewComponent(nameof(CommentExcuseDuty), Id);
       
        [HttpGet]
        public IActionResult PrintExcuseDuty(string Id) => ViewComponent(nameof(PrintExcuseDuty), Id);

    }
}