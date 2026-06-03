using System.Collections.Generic;

namespace DMX.ViewModels
{
    public class UserPermissionVM
    {
        public string RoleId { get; set; }
        public IList<RoleClaimsVM> RoleClaims { get; set; }
    }

}
