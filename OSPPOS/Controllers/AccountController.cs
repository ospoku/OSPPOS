using AspNetCoreHero.ToastNotification.Abstractions;
using DMX.Data;
using DMX.Models;
using DMX.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;

namespace DMX.Controllers
{
    public class AccountController(XContext dContext, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signinmanager, IWebHostEnvironment environment, INotyfService  notification, IDataProtectionProvider protectionProvider) : Controller
    {
        public readonly XContext dcx = dContext;
        public readonly INotyfService notyf = notification;
        public readonly UserManager<AppUser> usm = userManager;
        public readonly RoleManager<AppRole> rol = roleManager;
        public readonly SignInManager<AppUser> sim = signinmanager;
        public readonly IWebHostEnvironment env = environment;
        public readonly IDataProtector protector = protectionProvider.CreateProtector("IdProtector");

        [HttpGet]
        public IActionResult AddUser()
        {
            return ViewComponent(nameof(AddUser));
        }

       

        [HttpGet]
        public IActionResult EditUser(string Id)
        {
            return ViewComponent(nameof(EditUser), Id);
        }
        public IActionResult DeleteUser()
        {
            return ViewComponent(nameof(DeleteUser));
        }
  

        public IActionResult ViewUsers()
        {
            return ViewComponent(nameof(ViewUsers));
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(LoginVM loginVM)
        {
            return View(loginVM);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
                return View(loginVM);

            var result = await sim.PasswordSignInAsync(
                loginVM.Username,
                loginVM.Password,
                loginVM.RememberMe,
                lockoutOnFailure: false);

            if (result.Succeeded)
            {

                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View(loginVM);
        }




        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
        public async Task<IActionResult> ForgetPassword() =>  View();
        [HttpGet]
        public async Task<IActionResult>UserProfile()
        {
            return ViewComponent(nameof(UserProfile));
        }
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            return ViewComponent(nameof(EditProfile));
        }
        [HttpPost]
        public async Task<IActionResult>EditProfile(EditProfileVM editProfileVM,IFormFile? formFile)
        {
            AppUser profileToEdit= (from u in usm.Users
                                    where u.Id == usm.GetUserId(HttpContext.User)
                                    select u).FirstOrDefault();

           

            profileToEdit.Firstname = editProfileVM.Firstname;
            profileToEdit.Lastname = editProfileVM.Lastname;
            profileToEdit.PhoneNumber = editProfileVM.Telephone;

            if (formFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {


                    await formFile.CopyToAsync(memoryStream);


                    profileToEdit.Picture = memoryStream.ToArray();

                }
            }

            await usm.UpdateAsync(profileToEdit);

            notyf.Success("Profile successfully updated",5);

            return  RedirectToActionPermanent("Login");
        }
      
        [HttpPost]
        public async Task<IActionResult> DeletePhoto(string Id)
        {
            var photoToDelete = (from u in usm.Users where u.Id == protector.Protect(Id) select u).FirstOrDefault();
            photoToDelete.Picture=null;

            await usm.UpdateAsync(photoToDelete);
            notyf.Success("Photo successfully deleted", 5);
            return RedirectToActionPermanent("UserProfile");
        }
        [AllowAnonymous]
        public IActionResult Splash()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "Invalid verification link.";
                return RedirectToAction("Login");
            }

            var user = await usm.FindByIdAsync(userId);
            if (user == null)
            {
                TempData["Error"] = "User not found.";
                return RedirectToAction("Login");
            }

            var result = await usm.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                TempData["Message"] = "Email successfully verified! You can now login.";
                return RedirectToAction("Login");
            }
            else
            {
                TempData["Error"] = "Email verification failed.";
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResendVerificationEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                TempData["Error"] = "Please provide a valid email address.";
                return RedirectToAction("Login");
            }

            var user = await usm.FindByEmailAsync(email);
            if (user == null)
            {
                TempData["Error"] = "No user found with this email.";
                return RedirectToAction("Login");
            }

            if (user.EmailConfirmed)
            {
                TempData["Message"] = "Email is already confirmed. You can log in.";
                return RedirectToAction("Login");
            }

            try
            {
                // Generate token and confirmation link
                var token = await usm.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action(
                    nameof(VerifyEmail),
                    "Account",
                    new { userId = user.Id, token },
                    Request.Scheme);

                // Read email template
                string body = await System.IO.File.ReadAllTextAsync(
                    Path.Combine(env.WebRootPath, "Templates", "EmailTemplate", "EmailConfirmationTemplate.cshtml"));
                body = body.Replace("{UserName}", user.UserName).Replace("{url}", confirmationLink);

                // Send email
                using var mailMessage = new MailMessage();
                mailMessage.Subject = "Email Verification";
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(user.Email);
                mailMessage.From = new MailAddress("ospoku@gmail.com");
                mailMessage.Body = body;

                using var smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("ospoku@gmail.com", "az36400@osp"),
                    EnableSsl = true
                };

                await smtp.SendMailAsync(mailMessage);

                TempData["Message"] = "Verification email resent successfully. Please check your inbox.";
            }
            catch (SmtpException)
            {
                TempData["Error"] = "Unable to send email. Please check your internet connection and try again.";
            }

            return RedirectToAction("Login");
        }

    }
}
