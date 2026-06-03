using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace DMX.ViewModels
{
    public class DetailExcuseDutyVM
    {

        public string PatientName { get; set; }
        public string PatientId { get; set; }
        public DateTime DateofDischarge { get; set; }
        public string Diagnosis { get; set; }
        public int ExcuseDays { get; set; }
        public List<string> SelectedUsers { get; set; }
        public SelectList UsersList { get; set; }
    }
}
