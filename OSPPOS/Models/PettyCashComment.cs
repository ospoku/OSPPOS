using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DMX.Data;

namespace DMX.Models
{
    public class PettyCashComment:TableAudit
    {
        [Key]
        public int Id { get; set; }
        public Guid PublcId { get; set; }=Guid.NewGuid();

        public string Message { get; set; }
        public int PettyCashId { get; set; }
        public string UserId { get; set; }
        public PettyCash PettyCash { get; set; }
        public AppUser AppUser { get; set; }
    }
}
