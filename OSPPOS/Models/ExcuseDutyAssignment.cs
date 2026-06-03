using DMX.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMX.Models
{
    public class ExcuseDutyAssignment : TableAudit
    {
        [Key]
       
        public int Id { get; set; }
        public Guid PublicId { get; set; }
        public int ExcuseDutyId { get; set; }
        [ForeignKey("ExcuseDutyId")]
        public ExcuseDuty ExcuseDuty { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser AppUser { get; set; }
        public bool IsRead { get; set; } = false;

    }
}
