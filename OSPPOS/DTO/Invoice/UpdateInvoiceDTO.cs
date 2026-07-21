using Microsoft.AspNetCore.Mvc.Rendering;
using OSPPOS.Data;
using System.ComponentModel.DataAnnotations;

namespace OSPPOS.DTO.Invoice
{
    public class UpdateInvoiceDTO :TableAudit
    {
        public DateTime? DueDate { get; set; }
        // Product Details
        [Required(ErrorMessage = "Product name is required")]
        [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
        public int CustomerId { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }
        public decimal Discount { get; set; }
    }
}
