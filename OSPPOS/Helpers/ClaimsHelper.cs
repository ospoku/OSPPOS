using System.Reflection;
using System.Security.Claims;
using DMX.Models;
using DMX.ViewModels;
using Microsoft.AspNetCore.Identity;
using OSPPOS.Models;

namespace OSPPOS.Helpers
{
    public static class ClaimsHelper
    {
        public static void GetPermissions(this List<RoleClaimsVM> allPermissions, Type policy, string roleId)
        {
            FieldInfo[] fields = policy.GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (FieldInfo fi in fields)
            {
                allPermissions.Add(new RoleClaimsVM { Value = fi.GetValue(null).ToString(), Type = "Permissions" });
            }


        }


        //    public static async Task GetPermissions(
        //this List<RoleClaimsVM> allPermissions,
        //Type policy,
        //string roleId,
        //RoleManager<AppRole> roleManager)
        //    {
        //        // 1. Load all available permissions from your static permissions class
        //        FieldInfo[] fields = policy.GetFields(BindingFlags.Static | BindingFlags.Public);
        //        var permissionValues = fields.Select(fi => fi.GetValue(null).ToString()).ToList();

        //        // 2. Load existing claims for the role
        //        var role = await roleManager.FindByIdAsync(roleId);
        //        var roleClaims = await roleManager.GetClaimsAsync(role);
        //        var existingClaimValues = roleClaims.Where(c => c.Type == "Permissions")
        //                                            .Select(c => c.Value)
        //                                            .ToList();

        //        // 3. Combine: mark which permissions are already selected
        //        foreach (var permission in permissionValues)
        //        {
        //            allPermissions.Add(new RoleClaimsVM
        //            {
        //                Value = permission,
        //                Type = "Permissions",
        //                Selected = existingClaimValues.Contains(permission)
        //            });
        //        }
        //    }



        //    public static async void GetPermissions(
        //this List<RoleClaimsVM> allPermissions,
        //Type policy,
        //string roleId)
        //    {
        //        // 1. Load static permission fields
        //        FieldInfo[] fields = policy.GetFields(BindingFlags.Static | BindingFlags.Public);

        //        foreach (FieldInfo fi in fields)
        //        {
        //            allPermissions.Add(new RoleClaimsVM
        //            {
        //                Value = fi.GetValue(null).ToString(),
        //                Type = "Permissions"
        //            });
        //        }

        //        // 2. Load existing claims for the role
        //        var role = await roleManager.FindByIdAsync(roleId);
        //        var roleClaims = await roleManager.GetClaimsAsync(role);

        //        var existingClaimValues = roleClaims
        //            .Where(c => c.Type == "Permissions")
        //            .Select(c => c.Value)
        //            .ToList();

        //        // 3. Mark selected permissions
        //        foreach (var p in allPermissions)
        //            p.Selected = existingClaimValues.Contains(p.Value);
        //    }

        public static async Task AddPermissionClaim(this RoleManager<AppRole> roleManager, AppRole role, string permission)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
            {
                await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
                
            }
        }


    }
}