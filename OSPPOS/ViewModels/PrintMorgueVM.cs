using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMX.ViewModels
{
    public class PrintMorgueVM
    { public decimal AccruedFees { get; set; }

        public string PatientId { get; set; } = string.Empty;
        public string ReferenceNumber { get; set; } = string.Empty;
        public string Deceased { get; set; } = string.Empty;        
        public string Depositor { get; set; } = string.Empty;
        public string DepositorAddress { get; set; } = string.Empty;
        public string FinalDiagnoses { get; set; } = string.Empty;
        public string WardInCharge { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string TagNo { get; set; } = string.Empty;
        public string FolderNo { get; set; } = string.Empty;
        public int DeceasedTypeId { get; set; } 
        public SelectList DeceasedTypes { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
