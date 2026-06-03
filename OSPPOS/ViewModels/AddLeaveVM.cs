using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMX.ViewModels
{
    public class AddLeaveVM
    {
        public string LeaveId { get; set; }
        public List<string> SelectedUsers { get; set; }
        public SelectList UsersList { get; set; }
        public string From {  get; set; }
        public string To { get; set; }  
    }
}
