using DMX.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMX.Models
{
    public class FeeStructure : TableAudit
    {
        [Key]
        public int Id { get; set; }
        public Guid PublicId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Minimum days is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Minimum days must be a non-negative number.")]
        public int MinDays { get; set; }

        [Required(ErrorMessage = "Maximum days is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Maximum days must be a non-negative number.")]
        public int MaxDays { get; set; }

        [Required(ErrorMessage = "Fee is required.")]
        [Range(1, double.MaxValue, ErrorMessage = "Fee must be a non-negative number.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Fee { get; set; }

        [Required(ErrorMessage = "Deceased type is required.")]
        public int DeceasedTypeId { get; set; } // True for died in ward, false for brought in dead
        [ForeignKey("DeceasedTypeId")]
        public DeceasedType DeceasedType { get; set; }
    }
}