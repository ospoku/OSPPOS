using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DMX.Data;

namespace DMX.Models
{
    public class SMSTask : TableAudit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string SMSTaskId { get; set; }
        public string MemberId { get; set; }
        public string Telephone { get; set; }
        public bool IsSent { get; set; }
    }
}
