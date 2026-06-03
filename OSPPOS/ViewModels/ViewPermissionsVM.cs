namespace DMX.ViewModels
{
    public class ViewPermissionsVM
    {
     public string PublicId {  get; set; }
        public string ModuleName { get; set; }
        public string ActionName { get; set; }
        public string Code {  get; set; }
       public string Description {  get; set; }
        public List<PermissionVM> RoleClaims { get; set; }
    }
}
