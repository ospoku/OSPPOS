using OSPPOS.Data;
using System.ComponentModel.DataAnnotations;

namespace OSPPOS.Models
{
    public class Unit:TableAudit
    {
        [Key]
        public int UnitId { get; set; }
        public string Code { get; set; }
   
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ICollection<Product> Products { get; set; } = [];
    }
}
