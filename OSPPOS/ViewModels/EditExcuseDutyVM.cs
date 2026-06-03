using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DMX.ViewModels
{
    public class EditExcuseDutyVM 
    {
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [DataType(DataType.Text)]
        public string PatientId { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateofDischarge { get; set; }
        [DataType(DataType.Text)]
        public string Diagnosis { get; set; }
        
        public int ExcuseDays { get; set; }
        public List<string> SelectedUsers { get; set; }
        public SelectList UsersList { get; set; }
    }
}
