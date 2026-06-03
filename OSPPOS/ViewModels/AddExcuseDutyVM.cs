using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMX.ViewModels
{
    public class AddExcuseDutyVM
    {

        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string PatientName { get; set; }
        public string PatientId { get; set; }
        public DateTime DateofDischarge { get; set; }
        public string Diagnosis { get; set; }
        public int ExcuseDays { get; set; }
        public List<string> SelectedUsers { get; set; }
        public SelectList UsersList { get; set; }
    }
}
