using DMX.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMX.Models
{
    public class ServiceRequest:TableAudit
    {

        [Key]
        public int ServiceRequestId { get; set; }
        public Guid PublicId { get; set; } = Guid.NewGuid();
        public string RequestNumber { get; set; } = "SR" + Guid.NewGuid().ToString("N").Substring(0, 5);

     public string Title { get; set; }
       
        
           public string PriorityId { get; set; }
        [ForeignKey(nameof(ServiceRequest.PriorityId))]
        public Priority Priority { get; set; } // Priority level (Low, Medium, High, Emergency)
            public string Description { get; set; } // Detailed description of the request
           
            public byte[]? Attachments { get; set; } // File paths or links to attachments
       public string RequestTypeId { get; set; }
        [ForeignKey(nameof(ServiceRequest.RequestTypeId))]
        public RequestType RequestType { get; set; }
        public string CategoryId { get; set; }
        [ForeignKey(nameof(ServiceRequest.CategoryId))]
        public Category Category { get; set; }
        [ForeignKey(nameof(ServiceRequest.StatusId))]
           public Status Status { get; set; }
            public string StatusId { get; set; } // Current status (Pending, In Progress, Completed, Cancelled)
       
        public virtual ICollection<ServiceComment>Comments { get; set; }    
       
      
    }
}
