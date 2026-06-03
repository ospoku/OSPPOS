using DMX.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMX.Models
{
    public class PerDiem:TableAudit
    {
        public PerDiem()
        {

        }
        [Key]
        public int Id { get; set; }
        public Guid PerDiemId { get; set; }
        [ForeignKey(nameof(UserId))]
        public AppUser AppUser { get; set; }
       
        public string UserId { get; set; }
        [Precision(10, 4)]
        public decimal Amount { get; set; } = 0;
    }
}
