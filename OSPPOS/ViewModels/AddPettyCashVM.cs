using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DMX.ViewModels
{
    public class AddPettyCashVM
    {
        public string PettyCashId { get; set; }
        public List<string> SelectedUsers { get; set; }
        public SelectList UsersList { get; set; }
        public string Purpose { get; set; }


        [Precision(10, 4)]
        public decimal Amount { get; set; } = 0;
        
        public decimal Maximum { get; set; }
    }
}
