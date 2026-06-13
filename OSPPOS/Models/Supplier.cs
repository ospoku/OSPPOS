using OSPPOS.Data;
using System.ComponentModel.DataAnnotations;

namespace OSPPOS.Models
{
    public class Supplier:TableAudit
    {
        [Key]
            public int SupplierId { get; set; }
            public string Name { get; set; } = string.Empty;
         
            public string? Phone { get; set; }
            public string? Email { get; set; }
            public string? Address { get; set; }
            public bool IsActive { get; set; } = true;
            

            public ICollection<Product> Products { get; set; } = [];
            public ICollection<StockBatch> StockBatches { get; set; } = [];
        }
    }

