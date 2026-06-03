namespace DMX.ViewModels
{
    public class ManageUsRolesVM    {
        public string UserId { get; set; }
        public IList<UserRolesVM> UserRoles { get; set; }
    }

    public class UserRolesVM
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public bool Selected { get; set; }
    }
}