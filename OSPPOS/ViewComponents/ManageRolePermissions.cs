
using DMX.Constants;
using DMX.Data;

using OSPPOS.Helpers;
using DMX.Models;
using DMX.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Web;
namespace OSPPOS.ViewComponents
{
    //public class ManageRolePermissions(RoleManager<AppRole>roleManager, XContext xContext) : ViewComponent
    //{
    //    public readonly RoleManager<AppRole> rol = roleManager;
    //    public readonly XContext ctx=xContext;
    //    public async Task<IViewComponentResult> InvokeAsync(string Id)
    //    {
    //        var decodedId = HttpUtility.UrlDecode(Id)?.Replace(" ", "+"); // sanitize
    //        var decryptedId = protector.Unprotect(decodedId);
    //        var model = new RolePermissionVM();
    //        var allPermissions = new List<RoleClaimsVM>();

    //        allPermissions.GetPermissions(typeof(Permissions), decryptedId);
    //        var role = await rol.FindByIdAsync(decryptedId);
    //        model.RoleId = decryptedId;
    //        model.AvailableClaims = new SelectList(ctx.Permissions.ToList(), nameof(Permission.PermissionId), nameof(Permission.Code), nameof(Permission.Module)).ToList();
    //        var claims = await rol.GetClaimsAsync(role);
    //        var allClaimValues = allPermissions.Select(a => a.Value).ToList();
    //        var roleClaimValues = claims.Select(a => a.Value).ToList();
    //        var authorizedClaims = allClaimValues.Intersect(roleClaimValues).ToList();
    //        foreach (var permission in allPermissions)
    //        {
    //            if (authorizedClaims.Any(a => a == permission.Value))
    //            {
    //                permission.Selected = true;
    //            }
    //        }
    //        model.ModulePermissions = allPermissions
    //.GroupBy(p => p.Module)
    //.Select(g => new ModulePermissionVM
    //{
    //    Module = g.Key,
    //    Permissions = g.ToList()
    //})
    //.ToList();

    //        model.RoleClaims = allPermissions;
    //        return View(model);
    //    }
    //}








    public class ManageRolePermissions : ViewComponent
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly XContext _ctx;
        public readonly IDataProtector protector;
        public ManageRolePermissions(RoleManager<AppRole> roleManager, XContext ctx,IDataProtectionProvider provider)
        {
            _roleManager = roleManager;
            _ctx = ctx;
            protector = provider.CreateProtector("IdProtector");
        }

        public async Task<IViewComponentResult> InvokeAsync(string encryptedId)
        {
            
            var roleId = protector.Unprotect(encryptedId);

            var model = new RolePermissionVM();
            model.RoleId = roleId;

            var role = await _roleManager.FindByIdAsync(roleId);
            model.RoleName = role?.Name ?? "";

            // Load all permissions from DB
            var allPermissions = await _ctx.Permissions
                .OrderBy(p => p.Module).ThenBy(p => p.Action)
                .Select(p => new RoleClaimsVM
                {
                    Module = p.Module,
                    Action = p.Action,
                    Value = p.Code,
                    Selected = false
                })
                .ToListAsync();

            // Role claims (permission claims only). Use claim type "Permissions" if that's what you used.
            var claims = await _roleManager.GetClaimsAsync(role);
            var roleClaimValues = claims.Where(c => c.Type == "Permission").Select(c => c.Value).ToHashSet();

            // Mark selected
            foreach (var p in allPermissions)
            {
                p.Selected = roleClaimValues.Contains(p.Value);
            }

            model.RoleClaims = allPermissions;

            // Group by module for matrix UI (one row per module)
            model.ModulePermissions = allPermissions
                .GroupBy(x => x.Module)
                .Select(g => new ModulePermissionVM
                {
                    Module = g.Key,
                    Permissions = g.OrderBy(p => p.Action).ToList()
                })
                .ToList();

            return View(model); // Views/Shared/Components/ManageRolePermissions/Default.cshtml
        }
    }
}

