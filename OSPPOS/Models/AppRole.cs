using Microsoft.AspNetCore.Identity;

namespace DMX.Models
{
    public class AppRole : IdentityRole
    {
       
        public string Rolename { get; set; }
        public bool IsDeleted {  get; set; }
        public string Description { get; set; }
    }
}
