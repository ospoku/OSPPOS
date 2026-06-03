using DMX.Data;

namespace DMX.Models
{
    public class Leave : TableAudit
    {
        public string LeaveId { get; set; }
       
    public virtual ICollection<LeaveComment>Comments { get; set; }  
    }
}
