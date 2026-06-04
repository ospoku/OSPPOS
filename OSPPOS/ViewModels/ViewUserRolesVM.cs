using DMX.Models;

namespace OSPPOS.ViewModels
{
    public class ViewUserRolesVM
    {
        public string UserId { get; set; }
        public List<string> Roles { get; set; }
        public string Name { get; set; }
    }
}
