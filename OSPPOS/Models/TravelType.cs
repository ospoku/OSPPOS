using DMX.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMX.Models
{
    public class TravelType:TableAudit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public string TravelTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code {  get; set; }
    }
}
