using System.ComponentModel.DataAnnotations;

namespace OSPPOS.Data
{
    public class TableAudit
    {
        public Guid PublicId = new();
        public bool IsDeleted { get; set; }
        public string? CreatedBy { get; set; } 
        [DataType(DataType.Date)]
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ModifiedDate { get; set; }
       
    }
}
