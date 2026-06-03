using System.ComponentModel.DataAnnotations;

namespace DMX.Data
{
    public class TableAudit
    {

        public bool IsDeleted { get; set; }
        public string? CreatedBy { get; set; } 
        [DataType(DataType.Date)]
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ModifiedDate { get; set; }
       
    }
}
