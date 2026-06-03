using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DMX.Data;

namespace DMX.Models
{
    public class MemoAssignment : TableAudit
    {
        [Key]

        public int Id { get; set; }
        public Guid PublicId { get; set; } = Guid.NewGuid();

    
       
        public int MemoId { get; set; }
        [ForeignKey("MemoId")]
        public Memo Memo { get; set; }  
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser AppUser { get; set; }
        public bool IsRead { get; set; } = false;

    }
}
