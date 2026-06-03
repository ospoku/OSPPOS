using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMX.ViewModels
{
    public class AddTravelRequestVM
    {
        
      
        public SelectList TravelTypes { get; set; }
        public string TravelTypeId { get; set; }
        
        public DateTime EndDate { get; set; }
       public DateTime StartDate { get; set; }
        public DateTime DateofReturn { get; set; }
       
       public SelectList TransportModes { get; set; }
        public string TransportModeId { get; set; }
        public string Purpose { get; set; }
        public int ConferenceFee { get; set; }
        public int FuelClaim { get; set; }
        public int OtherExpenses { get; set; }
        public string AdditionalNotes { get; set; }
        public SelectList UsersList { get; set; }
        public List<string> SelectedUsers { get; set; }
    }
}
