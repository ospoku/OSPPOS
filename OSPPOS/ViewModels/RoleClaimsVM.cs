namespace DMX.ViewModels
{
    public class RoleClaimsVM
    {
        public string Type { get; set; }
        
        public string Module { get; set; }      // e.g., "Users", "Products"
        public string Action { get; set; }      // e.g., "View", "Edit", "Delete"
        public string Value { get; set; }       // The permission code (e.g., "Users.View")
        public bool Selected { get; set; }
    }
}
