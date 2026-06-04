
using OSPPOS.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OSPPOS.Models
{
    public class Category : TableAudit
    {
    
     
        [Required]
        public string Code { get; set; }
        [Required]
        public  string? Description { get; set; }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;           // e.g. "Soft Drinks", "Hard Drinks"
    
        public bool IsActive { get; set; } = true;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }

}

