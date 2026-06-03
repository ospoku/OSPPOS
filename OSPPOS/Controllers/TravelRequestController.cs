using AspNetCoreHero.ToastNotification.Abstractions;
using DMX.Data;

using DMX.Models;
using DMX.Services;
using DMX.ViewComponents;
using DMX.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DMX.Controllers
{
    public class TravelRequestController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly XContext _context;
        private readonly INotyfService _notyfService;
        private readonly EntityService _entityService;
        private readonly IAuthorizationService _authorizationService; private readonly AssignmentService _assignmentService;
        public TravelRequestController(
            XContext context,
            UserManager<AppUser> userManager,
            INotyfService notyfService, IAuthorizationService authorizationService,
             AssignmentService assignmentService,
            EntityService entityService)
        {
            _userManager = userManager;
            _context = context; _assignmentService = assignmentService;
            _notyfService = notyfService;
            _entityService = entityService; _authorizationService = authorizationService;
        }

        [HttpGet]
        public IActionResult ViewTravelRequests() => ViewComponent(nameof(ViewTravelRequests));

        [HttpGet]
        public IActionResult AddTravelRequest() => ViewComponent(nameof(AddTravelRequest));

        [HttpPost]
        public async Task<IActionResult> AddTravelRequest(AddTravelRequestVM addTravelRequestVm)
        {
            if (addTravelRequestVm.SelectedUsers?.Any() != true)
            {
                _notyfService.Error("You must select at least one user for assignment.", 5);
                return RedirectToAction(nameof(ViewTravelRequests));
            }
            if (_context.PerDiems.Where(x=>x.UserId== _userManager.GetUserId(User)).Any() != true)
            {
                _notyfService.Error($"You must set perdiem rate for {_userManager.GetUserAsync(User).Result.Fullname} to allow for Travel request", 5);
                return RedirectToAction(nameof(ViewTravelRequests));
            }

            try
            {
                // Check for duplicate travel requests
                var existingRequest = await _context.TravelRequests
                    .FirstOrDefaultAsync(p =>
                        p.TravelTypeId.ToLower() == addTravelRequestVm.TravelTypeId.ToLower() &&
                        p.Purpose.ToLower() == addTravelRequestVm.Purpose.ToLower());

                if (existingRequest != null)
                {
                    _notyfService.Error("This record already exists.");
                    return RedirectToAction("ViewTravelRequests");
                }

                // Create new travel request
                var newTravelRequest = new TravelRequest
                {
                    EndDate = addTravelRequestVm.EndDate,
                    StartDate = addTravelRequestVm.StartDate,
                    
                    ConferenceFee = addTravelRequestVm.ConferenceFee,
                    FuelClaim = addTravelRequestVm.FuelClaim,
                    OtherExpenses = addTravelRequestVm.OtherExpenses,
                    TravelTypeId = addTravelRequestVm.TravelTypeId,
                    Purpose = addTravelRequestVm.Purpose
                };

                // Add travel request to the database
                bool requestAdded = await _entityService.AddEntityAsync(newTravelRequest, User);
                if (!requestAdded)
                {
                    _notyfService.Error("Failed to create travel request.", 5);
                    return RedirectToAction("Error", "Home", new { message = "Travel request creation failed." });
                }

                // Assign selected users to the travel request
                foreach (var userId in addTravelRequestVm.SelectedUsers)
                {
                    var assignment = new TravelRequestAssignment
                    {
                        TravelRequestId = newTravelRequest.TravelRequestId,
                        AppUserId = userId
                    };

                    bool assignmentAdded = await _entityService.AddEntityAsync(assignment, User);
                    if (!assignmentAdded)
                    {
                        _notyfService.Error($"Failed to assign user {userId}.", 5);
                    }
                }

                _notyfService.Success("Travel request and assignments successfully processed.", 5);
                return RedirectToAction("ViewTravelRequests");
            }
            catch (Exception ex)
            {
                _notyfService.Error("An unexpected error occurred.", 5);
                Console.Error.WriteLine(ex);
                return RedirectToAction("Error", "Home", new { message = "An unexpected error occurred." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CommentTravelRequest(string Id, TravelRequestCommentVM commentVm)
        {
            try
            {
                // Find the travel request by ID
                var travelRequest = await _context.TravelRequests
                    .FirstOrDefaultAsync(t => t.TravelRequestId ==  (Id));

                if (travelRequest == null)
                {
                    return NotFound();
                }

                // Create a new comment
                var newComment = new TravelRequestComment
                {
                    TravelRequestId = travelRequest.TravelRequestId,
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

                return RedirectToAction(nameof(ViewTravelRequests));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { message = "An error occurred while processing the request.", ex.Message });
            }
        }


        [HttpGet]
        public async Task<IActionResult> EditTravelRequestAsync(string Id)
        {
            var unprotectedId = (Id);
            var travel = await _context.TravelRequests.FirstOrDefaultAsync(m => m.TravelRequestId == unprotectedId);
            if (travel == null)
            {
                return NotFound();
            }

            var authorizationResult = await _authorizationService.AuthorizeAsync(User,travel, "TravelRequestOwnerPolicy");
            if (!authorizationResult.Succeeded)
            {
                _notyfService.Error("You do not have access to this resource!", 5);
                return Json(new { success = false, message = "You do not have access to this resource!" });
            }

            return ViewComponent(nameof(EditTravelRequest), Id);
        }

        [HttpPost]
        public async Task<IActionResult> EditTravelRequest(string id, EditMemoVM editMemoVm)
        {
            

            if (editMemoVm.SelectedUsers == null || !editMemoVm.SelectedUsers.Any())
            {
                _notyfService.Error("You must select at least one user for assignment.", 5);
                return RedirectToAction("ViewMemos");
            }

            try
            {
                if(!Guid.TryParse((id), out Guid memoGuid))
                {
                    _notyfService.Error("Invalid memo ID format.", 5);
                    return RedirectToAction("ViewMemos");
                }

                var memoToUpdate = await _context.Memos.FirstOrDefaultAsync(m => m.PublicId == memoGuid);
                if (memoToUpdate == null)
                {
                    _notyfService.Error("Memo not found.", 5);
                    return RedirectToAction("ViewMemos");
                }

                memoToUpdate.Content = editMemoVm.Content;
                memoToUpdate.Title = editMemoVm.Title;

                bool isEdited = await _entityService.EditEntityAsync(memoToUpdate, User);
                if (!isEdited)
                {
                    _notyfService.Error("Failed to update memo. Please try again.", 5);
                    return RedirectToAction("ViewMemos");
                }

                var existingAssignments = _context.MemoAssignments.Where(a => a.PublicId == memoGuid);
                _context.MemoAssignments.RemoveRange(existingAssignments);

                bool atLeastOneFailed = false;
                var failedUsers = new List<string>();

                foreach (var userId in editMemoVm.SelectedUsers)
                {
                    var assignment = new MemoAssignment
                    {
                        MemoId = memoToUpdate.Id,
                        UserId = userId
                    };

                    bool assignResult = await _assignmentService.AssignUsers(assignment, User);
                    if (!assignResult)
                    {
                        atLeastOneFailed = true;
                        failedUsers.Add(userId);
                    }
                }

                if (atLeastOneFailed)
                {
                    _notyfService.Warning($"Record updated, but some assignments failed: {string.Join(", ", failedUsers)}", 7);
                }
                else
                {
                    _notyfService.Success("Record successfully updated", 5);
                }

                return RedirectToAction("ViewMemos");
            }
            catch (Exception ex)
            {
                _notyfService.Error("An unexpected error occurred. Please try again.", 5);
                Console.WriteLine($"Error updating Memo: {ex.Message}");
                return RedirectToAction("Error", "Home", new { message = "An error occurred while processing the memo." });
            }
        }

        [HttpGet]
        public IActionResult CommentTravelRequest(string Id) => ViewComponent(nameof(CommentTravelRequest), Id);
    }
}