using DMX.Data;
using DMX.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMX
{
    public class Deceased : TableAudit
    { 
        [Key]
      public int DeceasedId { get; set; }

        public Guid PublicId { get; set; }= Guid.NewGuid();
        [Required]
        [RegularExpression(@"^[A-Za-z\s]+")]
        public  string Name { get; set; }
        [Required]
        public string FolderNo { get; set; }
        [Required]
        public string Diagnoses { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z\s]+")]
        public   string Depositor { get; set; }
        [Required]
        public  string DepositorAddress { get; set; }
        public int DeceasedTypeId { get; set; }
        [ForeignKey(nameof(DeceasedTypeId))]
        public DeceasedType DeceasedType { get; set; }
        
        [Required]
        public  string Description { get; set; }
        public  ICollection<DeceasedService> DeceasedServices { get; set; } = [];
        [Required]
        public  string TagNo { get; set; }
      
        [Required]
        [RegularExpression(@"^[A-Zz-a\s]+")]
        public  string WardInCharge { get; set; }
        public string ReferenceNumber { get; set; } = Guid.NewGuid().ToString("N").Substring(0,5);
        public  ICollection<DeceasedComment> DeceasedComments { get; set; } = [];
    
    }
}
