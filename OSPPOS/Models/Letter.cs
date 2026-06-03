using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DMX.Data;

namespace DMX.Models
{
    public class Letter : TableAudit
    {
        
        [Key]
  public int Id { get; set; }
        public  Guid PublicId { get; set; }= Guid.NewGuid();
        public  string Source { get; set; }
        public  string Subject {  get; set; }
        public bool NeedReply {  get; set; }
        public DateTime DateReceived { get; set; }
        public DateTime DocumentDate { get; set; }
        public string ReferenceNumber { get; set; } = "L"+Guid.NewGuid().ToString("N").Substring(0,5);
        public string? AdditionalNotes { get; set; }
        [Required]
        public byte[] PDF { get; set; } = [];
        
        public ICollection<LetterComment> LetterComments { get; set; } = [];

       
    }
}
