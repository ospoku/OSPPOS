using DMX.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMX.Models
{
    public class MemoComment : TableAudit
    {
        [Key]
        public int Id { get; set; }
        public Guid PublicId { get; set; } = Guid.NewGuid();
        public string Message { get; set; }
        public int MemoId {  get; set; } 
        public string UserId { get; set; }
        [ForeignKey("MemoId")]
        public Memo Memo { get; set; }
        [ForeignKey("UserId")]

        public AppUser AppUser { get; set; }
    }
}
