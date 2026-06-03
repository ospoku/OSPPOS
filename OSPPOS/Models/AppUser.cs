using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OSPPOS.Models
{
    public class    AppUser : IdentityUser
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }

        public string Fullname
        {
            get
            {
                return Firstname
                    + "  "
                    + Lastname;
            }
        }


    
        public bool IsDeleted { get; set; }

        [Precision(10, 4)]
        public decimal Rate { get; set; } = 0;

     
        public string? DepartmentId { get; set; }
        public string? RankId { get; set; }
        public byte[]? Picture { get; set; }

     
        public string FullName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<StockBatch> StockBatches { get; set; } = new List<StockBatch>();
        public ICollection<SaleOrder> SaleOrders { get; set; } = new List<SaleOrder>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }


}

    
}
