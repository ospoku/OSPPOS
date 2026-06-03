using DMX.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMX.Models
{
    public class MorgueService:TableAudit
    {
        [Key]
        public int Id { get; set; }
        public Guid MorgueServiceId { get; set; }
        public string ServiceName { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        [Precision(4)]
        public decimal Amount { get; set; }

    }
}
