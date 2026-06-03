using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DMX.ViewModels
{
    public class RolePermissionVM
    {
        
     
        public string RoleId { get; set; }
        public string RoleName {  get; set; }
        public IList<RoleClaimsVM> RoleClaims { get; set; }
        public List<string>? SelectedClaimValues { get; set; }

        // NEW grouped list for UI table
        public List<ModulePermissionVM> ModulePermissions { get; set; }
    }

}
