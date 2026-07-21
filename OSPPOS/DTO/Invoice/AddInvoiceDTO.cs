using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace OSPPOS.DTO.Invoice
{
    public class AddInvoiceDTO
    {    // Select Lists
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
   
  
        // Product Details
    
        public int CustomerId { get; set; }

     
        public string? Notes { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public decimal Discount { get; set; }
       public string WalkInCustomer { get; set; } = string.Empty;

       

    }
}
