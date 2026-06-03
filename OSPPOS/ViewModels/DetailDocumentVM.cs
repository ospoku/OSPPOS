using DMX.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DMX.ViewModels
{
    public class DetailLetterVM
    {
       
        public string PatientId { get; set; }

        public string FinalDiagnoses { get; set; }
        public string WardInCharge { get; set; }
        public DateTime Date { get; set; }
        public decimal AmountDue {  get; set; } 
        public string ReferenceNumber { get; set; }
        public virtual ICollection<DeceasedComment> PatientComments { get; set; }

    }
}
