using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMX.Models
{
    public class EditAnnualLeaveVM
    {
        public string ALeavesId { get; set; }
        public List<string> SelectedUsers { get; set; }
        public SelectList UsersList { get; set; }
    }
}
