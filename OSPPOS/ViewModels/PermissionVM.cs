using DMX.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DMX.ViewModels
{
    public class PermissionVM
    {
        public int PermissionId { get; set; }
        public string PublicId { get; set; } 
        public string RoleId { get; set; }
        public string Module { get; set; }
        public string Action { get; set; }
        
        public string Code { get; set; }
    

        // Add this property for UI selection
        public List <SelectListItem>AvailableClaims  { get; set; }
    }

}
