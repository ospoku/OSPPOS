using AspNetCoreHero.ToastNotification.Abstractions;

using DMX.Data;


using DMX.Helpers;
using DMX.Models;
using DMX.Services;
using DMX.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Web;


namespace DMX.Controllers
{
    public class PermissionController(XContext xContext, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signinmanager, IWebHostEnvironment environment, EntityService entityService, INotyfService notyfService, IDataProtectionProvider dataProvider) : Controller
    {
        public readonly XContext xct = xContext;
        public readonly UserManager<AppUser> usm = userManager;
        public readonly RoleManager<AppRole> rol = roleManager;
        public readonly SignInManager<AppUser> sim = signinmanager;
        public readonly IWebHostEnvironment env = environment;
        public readonly EntityService ens = entityService;
        public readonly INotyfService notyf = notyfService;
        public readonly IDataProtector protector = dataProvider.CreateProtector("IdProtector");

        [HttpGet]
        public IActionResult AddUser()
        {
            return ViewComponent(nameof(AddUser));
        }

        public IActionResult UserManagement()
        {
            return ViewComponent(nameof(UserManagement));
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserVM addUserVM)
        {
            if (!ModelState.IsValid)
            {
                TempData["ResultMessage"] = "User creation error: Invalid data.";
                return RedirectToAction("AddUser");
            }

            // 1️⃣ Create the user
            var newUser = new AppUser
            {
                UserName = addUserVM.Username,
                Email = addUserVM.Email,
                PhoneNumber = addUserVM.Telephone,
                Firstname = addUserVM.Firstname,
                Lastname = addUserVM.Lastname
            };

            var createResult = await usm.CreateAsync(newUser, addUserVM.Password);

            if (!createResult.Succeeded)
            {
                TempData["ResultMessage"] = "User creation failed: " + string.Join(", ", createResult.Errors.Select(e => e.Description));
                return RedirectToAction("AddUser");
            }

            // 2️⃣ Assign role if provided
            if (!string.IsNullOrEmpty(addUserVM.ApplicationRoleId))
            {
                var role = await rol.FindByIdAsync(addUserVM.ApplicationRoleId);
                if (role != null)
                {
                    await usm.AddToRoleAsync(newUser, role.Name);
                }
            }

            TempData["Message"] = "New User Created Successfully.";

            

            // 4️⃣ Send SMS notification
            //string smsUrl = "https://frog.wigal.com.gh/ismsweb/sendmsg?";
            //string smsMessage = $"username=KofiPoku&password=Az36400@osp&from=JHC&to=233244139692&service=SMS&message=Testing JHC Message Alerts";
            //using (var httpClient = new HttpClient())
            //{
            //    var response = await httpClient.PostAsync(smsUrl + smsMessage, null);
            //    // Optionally check response.StatusCode and handle errors
            //}

            return RedirectToAction("ViewUsers");
        }

        [HttpGet]
        public IActionResult EditUser(string Id)
        {
            return ViewComponent(nameof(EditUser), Id);
        }

        public IActionResult DetailUser(string Id)
        {
            return ViewComponent(nameof(DetailUser), Id);
        }
        public IActionResult DeleteUser()
        {
            return ViewComponent(nameof(DeleteUser));
        }
        [HttpPost]
        public async Task<IActionResult> EditUserAsync(string Id, EditUserVM editUserVM)
        {
            // Get the user by Id
            var searchUser = await usm.FindByIdAsync(Id);
            if (searchUser == null)
                return NotFound();

            // Update system-level fields only (admin-level edits)
            searchUser.Email = editUserVM.Email;
            searchUser.EmailConfirmed = editUserVM.EmailConfirmed;       // Checkbox
            searchUser.PhoneNumber = editUserVM.PhoneNumber;             // System phone number
            searchUser.UserName = editUserVM.Username;
            searchUser.LockoutEnabled = !editUserVM.IsActive;            // Deactivate / Activate account
            searchUser.LockoutEnd = editUserVM.IsActive ? null : DateTimeOffset.MaxValue;

           // Example

            // Apply updates
            var updateResult = await usm.UpdateAsync(searchUser);
            if (!updateResult.Succeeded)
            {
                // handle errors, maybe add ModelState errors
                TempData["Error"] = "Failed to update user.";
                return View("Users");
            }

            // Handle Roles (if changed)
            var currentRoles = await usm.GetRolesAsync(searchUser);
            if (currentRoles.SingleOrDefault() != editUserVM.ApplicationRoleId)
            {
                await usm.RemoveFromRolesAsync(searchUser, currentRoles);
                if (!string.IsNullOrEmpty(editUserVM.ApplicationRoleId))
                {
                    await usm.AddToRoleAsync(searchUser, editUserVM.ApplicationRoleId);
                }
            }

            // Handle Reset Password
            if (!string.IsNullOrEmpty(editUserVM.ResetPassword))
            {
                var token = await usm.GeneratePasswordResetTokenAsync(searchUser);
                await usm.ResetPasswordAsync(searchUser, token, editUserVM.ResetPassword);
            }

            // TODO: Handle Permissions (if your system supports claims/permissions)
            // await UpdateUserPermissions(searchUser, editUserVM.SelectedPermissions);

            TempData["Success"] = "User successfully updated.";
            return RedirectToAction("Users");
        }

        [HttpGet]
        public IActionResult ViewPermissions()
        {
            return ViewComponent(nameof(ViewPermissions));
        }
        public IActionResult ManageUserRoles(string Id)
        {

            return ViewComponent(nameof(ManageUserRoles), Id);
        }

 




     
        [HttpGet]
        public IActionResult ManagePermissions(string Id)
        {
            return ViewComponent(nameof(ManagePermissions),Id);

        }

        [HttpGet]
        public IActionResult ViewUserRoles()
        {
            return ViewComponent(nameof(ViewUserRoles));
        }
        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult ViewRoles()
        {
            return ViewComponent(nameof(ViewRoles));
        }

        [HttpGet]
        public IActionResult AddRole()
        {
            return ViewComponent(nameof(AddRole));
        }


        [HttpPost]
        public async Task<IActionResult> AddRole(AddRoleVM addRoleVM)
        {
            AppRole appRole = new()
            {
                Rolename = addRoleVM.Name,
                Name = addRoleVM.Name,
                Description = addRoleVM.Description,
            };

            await rol.CreateAsync(appRole);

            return RedirectToAction(nameof(ViewRoles));
        }
        [HttpGet]
        public IActionResult ManageRolePermissions(string Id)
        {


            return ViewComponent(nameof(ManageRolePermissions), Id);
        }
        [HttpGet]
        public IActionResult ManageUserPermissions(string Id)
        {


            return ViewComponent(nameof(ManageUserPermissions), Id);
        }
        [HttpPost]
   
        public async Task<IActionResult> ManageRolePermissions(RolePermissionVM model)
        {
            try
            {
                var roleId = protector.Unprotect(model.RoleId);
                var role = await rol.FindByIdAsync(roleId);

                var claims = await rol.GetClaimsAsync(role);

                // Remove old claims
                foreach (var claim in claims)
                {
                    await rol.RemoveClaimAsync(role, claim);
                }

                // Add new claims
                var selectedClaims = model.SelectedClaimValues?.ToList() ?? [];
                foreach (var claim in selectedClaims)
                {
                    await rol.AddPermissionClaim(role, claim);
                }
                var usersInRole = await userManager.GetUsersInRoleAsync(role.Id);
                foreach (var user in usersInRole)
                {
                    await sim.RefreshSignInAsync(user);
                }

                // ✅ Notify only if successful
                notyf.Success("Permissions successfully updated");
            }
            catch (Exception ex)
            {
                // ❌ Notify on error
                notyf.Error("Something went wrong while updating permissions");

                // Optional: log the error
                // _logger.LogError(ex, "Error updating role permissions");

                return RedirectToAction(nameof(UserManagement));
            }

            return RedirectToAction(nameof(UserManagement));
        }



        [HttpPost]
        public async Task<IActionResult> ManageUserRoles(string Id, ManageUserRolesVM model)
        {
            var unprotectedId = protector.Unprotect(Id);
            var user = await usm.FindByIdAsync((unprotectedId));
            var roles = await usm.GetRolesAsync(user);
            var result = await usm.RemoveFromRolesAsync(user, roles);
            result = await usm.AddToRolesAsync(user, model.UserRoles.Where(x => x.Selected).Select(y => y.RoleName));
            return RedirectToAction(nameof(ViewUserRoles));
        }
        [HttpPost]
        public async Task <IActionResult> AddPermission(AddPermissionVM addPermissionVM)
        {
            Permission addThisPermission = new()
            {
                Action = addPermissionVM.ActionName,
                Module=addPermissionVM.ModuleName,
                Description= addPermissionVM.Description,
            };
            bool result = await ens.AddEntityAsync(addThisPermission, User);
            if (!result)
            {
                notyf.Error("An error occurred while processing the request.", 5);
              
                return ViewComponent(nameof(UserManagement),addPermissionVM);
            }
            else
            {
                notyf.Success("Permission successfully created");

                return ViewComponent(nameof(UserManagement));
            }
        }
        


    }
}