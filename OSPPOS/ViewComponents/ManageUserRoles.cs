using DMX.Data;

using DMX.Models;
using DMX.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared;
using System.Web;

namespace DMX.ViewComponents
{
    public class ManageUserRoles(XContext dContext, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager,IDataProtectionProvider provider) : ViewComponent
    {
        public readonly XContext dcx = dContext;
        public readonly UserManager<AppUser> usm = userManager;
        public readonly RoleManager<AppRole> rol = roleManager;
        public readonly IDataProtector protector = provider.CreateProtector("IdProtector");
        public async Task<IViewComponentResult> InvokeAsync(string Id)
        {
            var viewModel = new List<UserRolesVM>();
            var decodedId = HttpUtility.UrlDecode(Id)?.Replace(" ", "+"); // sanitize
            var decryptedId = protector.Unprotect(decodedId);
            var user = await usm.FindByIdAsync(decryptedId);

            foreach (var role in rol.Roles.ToList())
            {
                var userRolesViewModel = new UserRolesVM
                {
                    RoleName = role.Name
                };
                if (await usm.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                viewModel.Add(userRolesViewModel);
            }
            var model = new ManageUserRolesVM()
            {
                UserId = @protector.Unprotect(Id),

                UserRoles = viewModel
            };
            return View(model);
        }
    }
}
