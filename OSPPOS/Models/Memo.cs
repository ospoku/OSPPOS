using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DMX.Data;

namespace DMX.Models
{
    public class Memo : TableAudit
    {
        [Key]
        public int Id { get; set; }



        public Guid PublicId { get; set; } = Guid.NewGuid();
        public string ReferenceId { get; set; }="M"+ Guid.NewGuid().ToString("N").Substring(0,5);
        public string Title { get; set; }
        public string Content { get; set; }
        
        public ICollection<MemoComment> MemoComments { get; set; }
    
      
    }
}
