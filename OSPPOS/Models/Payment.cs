using OSPPOS.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OSPPOS.Models
{
  
    public class Payment :TableAudit
    {
      
            public int Id { get; set; }

           
            public int? CustomerId { get; set; }

            public Customer? Customer { get; set; }
            public decimal Amount { get; set; }

            public PaymentMethod Method { get; set; }   // enum (Cash, MoMo, Card, Bank)

            public string? Reference { get; set; }      // MoMo / bank ref
            public string? Notes { get; set; }

            public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
            public int OrderId { get; set; }
            public SaleOrder Order { get; set; }

    }


    }



