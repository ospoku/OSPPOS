using DMX.Constants;

using OSPPOS.Helpers;
using DMX.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OSPPOS.Data;
using OSPPOS.Models;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{
    public class ViewRolePermissions : ViewComponent
    {
        private readonly XContext _context;
        private readonly RoleManager<AppRole> _roleManager;

        public ViewRolePermissions(XContext context, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var roles = _roleManager.Roles.ToList();

            var model = new List<ViewRolePermissionsVM>();

            foreach (var role in roles)
            {
                var claims = await _roleManager.GetClaimsAsync(role);

                var permissionCodes = claims
                    .Where(c => c.Type == "Permission")
                    .Select(c => c.Value)
                    .ToList();

                model.Add(new ViewRolePermissionsVM
                {
                    RoleId = role.Id,
                    RoleName = role.Rolename,
                    SelectedPermissions = permissionCodes,
                    
                    
                });
            }

            return View(model);
        }
    }
}