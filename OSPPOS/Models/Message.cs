using DMX.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMX.Models
{
    public class Message : TableAudit
    {
        public Message()
        {

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string MessageId { get; set; }
        public string Body { get; set; }
        public byte IsRead { get; set; }

        public string Sender { get; set; }
        public string Subject { get; set; }

        public string Reciever { get; set; }


    }
}
