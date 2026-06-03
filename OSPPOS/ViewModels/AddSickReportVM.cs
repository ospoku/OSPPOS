using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMX.ViewModels
{
    public class AddSickReportVM
    {
        public List<string> SelectedUsers { get; set; }
        public SelectList UsersList { get; set; }
        public string AdditionalNotes { get; set; }
    }
}
