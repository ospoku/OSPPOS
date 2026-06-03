using DMX.Models;

namespace DMX.ViewModels
{
    public class ViewInvoiceVM
    {
            public string DeceasedName { get; set; } // Example: Name of the deceased
            public DateTime CreatedDate { get; set; } // Date the deceased record was created
            public int TotalDays { get; set; } // Total number of days
            public decimal TotalInvoiceAmount { get; set; } // Total invoice amount
            public List<TierCharge> TierCharges { get; set; } // List of tier charges

        public decimal BaseFee { get; set; }
        public decimal ExtraServicesFee { get; set; }
       
        public List<MorgueService> SelectedServices { get; set; }

    }
}

