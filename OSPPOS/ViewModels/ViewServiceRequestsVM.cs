using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMX.ViewModels
{
    public class ViewServiceRequestsVM
    {
        
        public string Title {  get; set; }
        public Guid PublicId { get; set; }
            public string RequestNumber { get; set; }
            public string ServiceRequestedBy { get; set; }
            public DateTime RequestDate { get; set; }
            public string Unit { get; set; }
            public string Description { get; set; }

            public string FaultInspectedBy { get; set; }

            public string ActionToBeTaken { get; set; }
        public DateTime CreatedDate { get;  set; }
    }
    }




