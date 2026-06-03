using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DMX.Data;

namespace DMX.Models
{
    public class MaternityLeave:TableAudit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string MaternityLeaveId { get; set; }
        public string Name { get; set; }
        public int NumberWeeks { get; set; }

        public string EDD { get; set; }
         public DateTime LeaveDate { get; set; }
        public string MedicalOfficer { get; set; }
        public DateTime IssueDate { get; set; }
       
       public virtual ICollection<MaternityLeaveComment> Comments { get; set; } 
    }
}
