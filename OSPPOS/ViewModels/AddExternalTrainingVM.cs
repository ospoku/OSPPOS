
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMX.ViewModels
{
    public class AddExternalTrainingVM
    {
     
        public string WorkshopTitle { get;  set; }
        public int NumberofDays { get; set; }
        public DateTime DepartureDate { get;  set; }
        public DateTime ReturnDate { get;  set; }
        public DateTime TrainingDate { get;  set; }
        public SelectList Users { get; set; }   
        public List<string> SelectedUsers { get; set; } 
        public string Description { get;  set; }
    
  
    }
}
