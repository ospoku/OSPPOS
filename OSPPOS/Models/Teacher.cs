using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DMX.Data;
using Microsoft.EntityFrameworkCore;

namespace DMX.Models
{
    public class Teacher : TableAudit
    {[Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeacherId { get; set; }
        public string ReferenceNumber { get; set; } = "T" + Guid.NewGuid().ToString("N").Substring(0, 5);  
     public string FacultyId { get; set; }
        public string DepartmentId { get; set; }
        public string Name { get; set; }

      
 
      
   
    }
}
