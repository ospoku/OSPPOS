using DMX.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMX.Models
{
    public class DeceasedAssignment : TableAudit
    {
        [Key]
        public int DeceasedAssignmentId { get; set; }
        public Guid PublicId { get; set; }
        public int DeceasedId { get; set; }
        [ForeignKey(nameof(DeceasedId))]
        public Deceased Deceased { get; set; }
        public  string UserId { get; set; }
        [ForeignKey(nameof( UserId))]
        public AppUser AppUser { get; set; }
        public bool IsRead { get; set; } = false;

    }
}
