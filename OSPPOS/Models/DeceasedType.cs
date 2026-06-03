using DMX.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Permissions;

namespace DMX.Models
{
    public class DeceasedType:TableAudit
    {
        [Key]
     public int DeceasedTypeId { get; set; }
        public Guid PublicId { get; set; }
        [Required]
        public   string Name { get; set; }
        [Required]
        public  string Description { get; set; }
        [Required]
        public  string Code { get; set; }
    }
}
