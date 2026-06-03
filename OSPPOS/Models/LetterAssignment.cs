using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DMX.Data;

namespace DMX.Models
{
    public class LetterAssignment : TableAudit
    {
        [Key]
   public int Id { get; set; }
        public Guid PublicId { get; set; }= Guid.NewGuid();
        public int LetterId { get; set; }
        public Letter Letter { get; set; }  
        public string UserId { get; set; }
        public AppUser AppUser { get; set; }
        public bool IsRead { get; set; } = false;

    }
}
