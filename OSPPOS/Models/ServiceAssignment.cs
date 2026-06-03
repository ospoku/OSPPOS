using DMX.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMX.Models
{
    public class ServiceAssignment : TableAudit
    {
        [Key]
        public int ServiceAssignmentId { get; set; }
        public Guid PublicId { get; set; } = Guid.NewGuid();
        public int ServiceRequestId { get; set; }
        [ForeignKey(nameof(ServiceRequestId))]
        public ServiceRequest ServiceRequest { get; set; }
        public string UserId { get; set; }
        public AppUser AppUser { get; set; }
        public bool IsRead { get; set; } = false;

    }
}
