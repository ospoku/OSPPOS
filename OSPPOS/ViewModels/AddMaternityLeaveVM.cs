using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMX.ViewModels
{
    public class AddMaternityLeaveVM
    {
        public string MaternityLeaveId { get; set; }
        public string Name { get; set; }
        public int NumberWeeks { get; set; }

        public string EDD { get; set; }
        public DateTime LeaveDate { get; set; }
        public string MedicalOfficer { get; set; }
        public DateTime IssueDate { get; set; }
        public List<string> SelectedUsers { get; set; }
        public SelectList UsersList { get; set; }
    }
}
