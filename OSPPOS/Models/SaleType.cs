using OSPPOS.Data;
using System.ComponentModel.DataAnnotations;

namespace OSPPOS.Models
{
    public class SaleType:TableAudit
    {
        [Key]
        public int SaleTypeId { get; set; }
        public string Code { get; set; }
       public Guid PublicId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ICollection<SaleOrder> SaleOrders {get; set;}
    }
}
