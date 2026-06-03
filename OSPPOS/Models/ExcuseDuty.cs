using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DMX.Data;

namespace DMX.Models
{
    public class ExcuseDuty : TableAudit
    {[Key]  
        public int Id { get; set; }
        public Guid PublicId { get; set; }= Guid.NewGuid();
      
        public string PatientName { get; set; }
        public string PatientId { get; set; }
        public DateTime DateofDischarge { get; set; }
        public string Diagnosis { get; set; }
        public int ExcuseDays { get; set; }
        public ICollection<ExcuseDutyComment> ExcuseDutyComments { get; set; }
        public string ReferenceNumber { get; set; } = "E" + Guid.NewGuid().ToString("N").Substring(0, 5);
            

    }
}
