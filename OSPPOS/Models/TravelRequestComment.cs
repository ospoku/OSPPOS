using DMX.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DMX.Models
{
    public class TravelRequestComment:TableAudit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string Message { get; set; }
        public string TravelRequestId { get; set; }
        public string UserId { get; set; }
        public TravelRequest TravelRequest { get; set; }
        public AppUser AppUser { get; set; }
    }
}
