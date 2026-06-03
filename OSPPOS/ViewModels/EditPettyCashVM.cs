using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMX.ViewModels
{
    public class EditPettyCashVM
    {
        public string PettyCashId { get; set; }
        public List<string> SelectedUsers { get; set; }
        public SelectList UsersList { get; set; }
        public decimal Maximum { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Purpose { get; set; }
    }
}
