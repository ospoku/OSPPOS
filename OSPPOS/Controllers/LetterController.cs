using AspNetCoreHero.ToastNotification.Abstractions;
using DMX.Data;

using DMX.Models;
using DMX.Services;
using DMX.ViewComponents;
using DMX.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DMX.Controllers
{
    [Authorize]
    public class LetterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly XContext _context;
        private readonly INotyfService _notyfService;
        private readonly EntityService _entityService;
        private readonly AssignmentService _assignmentService;
        public readonly IDataProtector protector;
        public LetterController(
            XContext context,
            UserManager<AppUser> userManager,
            INotyfService notyfService,
            EntityService entityService,
            AssignmentService assignmentService, IDataProtectionProvider provider)
        {
            _userManager = userManager;
            _context = context;
            _notyfService = notyfService;
            _entityService = entityService;
            protector = provider.CreateProtector("IdProtector");
            _assignmentService = assignmentService;
        }

        [HttpGet]
        public IActionResult AddLetter() => ViewComponent(nameof(AddLetter));

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 104857600)] // 100MB limit
        public async Task<IActionResult> AddLetter(AddLetterVM addLetterVm, IFormFile formFile)
        {
            if (addLetterVm.SelectedUsers == null || !addLetterVm.SelectedUsers.Any())
            {
                _notyfService.Error("You must select at least one user for assignment.", 5);
                return RedirectToAction("ViewLetters");
            }

            try
            {
                if (formFile == null || formFile.Length == 0)
                {
                    _notyfService.Error("Please upload a valid document.");
                    return RedirectToAction("ViewLetters");
                }

                if (Path.GetExtension(formFile.FileName).ToLower() != ".pdf")
                {
                    _notyfService.Error("Only PDF documents are allowed.");
                    return RedirectToAction("ViewLetters");
                }

                var existingLetter = await _context.Letters.FirstOrDefaultAsync(l =>
                    l.Subject.ToLower() == addLetterVm.Subject.ToLower() &&
                    l.Source.ToLower() == addLetterVm.Source.ToLower() &&
                    l.DateReceived == addLetterVm.ReceiptDate &&
                    l.DocumentDate == addLetterVm.DocumentDate);

                if (existingLetter != null)
                {
                    _notyfService.Error("This record already exists.");
                    return RedirectToAction("ViewLetters");
                }

                var newLetter = new Letter
                {
                    Source = addLetterVm.Source,
                    Subject = addLetterVm.Subject,
                    DateReceived = addLetterVm.ReceiptDate,
                    DocumentDate = addLetterVm.DocumentDate,
                    AdditionalNotes = addLetterVm.AdditionalNotes
                };

                using (var memoryStream = new MemoryStream())
                {
                    await formFile.CopyToAsync(memoryStream);
                    newLetter.PDF = memoryStream.ToArray();
                }

                bool result = await _entityService.AddEntityAsync(newLetter, User);
                if (!result)
                {
                
                    _notyfService.Error("Document saving failed.");
                    return RedirectToAction("ViewLetters");
                }
                foreach (var userId in addLetterVm.SelectedUsers)
                {
                    var assignment = new LetterAssignment
                    {
                        LetterId = newLetter.Id,
                        UserId = userId
                    };

                    bool assignResult = await _assignmentService.AssignUsers(assignment, User);
                    if (!assignResult)
                    {
                        _notyfService.Error($"Failed to assign Letter to user {userId}.", 5);
                    }
                }

                _notyfService.Success("Letter and assignments successfully processed.", 5);
                return RedirectToAction(nameof(ViewLetters));
            }
            catch (Exception ex)
            {
                _notyfService.Error("An error occurred: " + ex.Message, 5);
                return RedirectToAction("Error", "Home", new { message = "An error occurred while processing the memo." });
            }
        }

        [HttpGet]
        public IActionResult EditLetter(Guid Id) => ViewComponent(nameof(EditLetter), Id);

        [HttpPost]
        public async Task<IActionResult> EditLetterAsync(Guid id, Letter letter, IFormFile formFile)
        {
            try
            {
                var letterToUpdate = await _context.Letters.FirstOrDefaultAsync(a => a.PublicId == letter.PublicId);
                if (letterToUpdate == null)
                {
                    return NotFound();
                }

                letterToUpdate.ReferenceNumber = letter.ReferenceNumber;
                letterToUpdate.DocumentDate = letter.DocumentDate;
                letterToUpdate.DateReceived = letter.DateReceived;
                letterToUpdate.Source = letter.Source;
                letterToUpdate.AdditionalNotes = letter.AdditionalNotes;

                if (formFile != null && formFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await formFile.CopyToAsync(memoryStream);
                        letterToUpdate.PDF = memoryStream.ToArray();
                    }
                }

                _context.Letters.Attach(letterToUpdate);
                _context.Entry(letterToUpdate).State = EntityState.Modified;

                if (await _context.SaveChangesAsync() > 0)
                {
                    _notyfService.Success("Record successfully updated.");
                    return RedirectToAction("ViewLetters");
                }
                else
                {
                    _notyfService.Error("Document saving failed.");
                    return ViewComponent(nameof(AddLetter));
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { message = "An error occurred while updating the document: " + ex.Message });
            }
        }

        [HttpGet]
        public IActionResult ViewLetters() => ViewComponent(nameof(ViewLetters));

        [HttpGet]
        public IActionResult DeleteLetter() => ViewComponent(nameof(ViewLetters));

        [HttpPost]
        public async Task<IActionResult> CommentLetter(DocumentCommentVM commentVm)
        {
            try
            {
             
                var letterToComment = await _context.Letters.FirstOrDefaultAsync(l => l.PublicId == commentVm.MemoId);
                if (letterToComment == null)
                {
                    return NotFound();
                }

                var newComment = new LetterComment
                {
                    LetterId = letterToComment.Id,
                    Message = commentVm.NewComment,
                    UserId = (await _userManager.GetUserAsync(User)).Id
                };

                bool result = await _entityService.AddEntityAsync(newComment, User);
                if (result)
                {
                    _notyfService.Success("Comment successfully saved!!!");
                }
                else
                {
                    _notyfService.Error("Comment could not be saved.");
                }

                return RedirectToAction("ViewLetters");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { message = "An error occurred while processing the comment: " + ex.Message });
            }
        }

        [HttpGet]
        public IActionResult CommentLetter(string id) => ViewComponent(nameof(CommentLetter), id);

        [HttpGet]
        public IActionResult PrintDocument(string id) => ViewComponent(nameof(PrintDocument), id);

        [HttpGet]
        public async Task<IActionResult> Download(string id)
        {
            var decryptedId = protector.Unprotect(id);
            if(!Guid.TryParse(decryptedId, out Guid letterGuid))
            {
                return BadRequest("Invalid document ID format.");
            }
            var letter = await _context.Letters.FirstOrDefaultAsync(m => m.PublicId == letterGuid);
            if (letter == null || letter.PDF == null)
            {
                return NotFound();
            }

            return new FileContentResult(letter.PDF, "application/octet-stream")
            {
                FileDownloadName = $"Document_{letter.ReferenceNumber}.pdf"
            };
        }
    }
}