using DMX.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMX.Models
{
    public class Subject : TableAudit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string SubjectId { get; set; }
        public string FacultyId { get;  set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DepartmentId { get; set; }
        public object Groups { get; internal set; }
        public int StudentPopulation { get; set; } // Number of students enrolled in this course
    }
}
