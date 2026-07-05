using OSPPOS.Enums;
using System.ComponentModel.DataAnnotations;

namespace OSPPOS.ViewModels
{
    public class RecordPaymentVM
    {
        [Required] public int SaleOrderId { get; set; }
        [Required, Range(0.01, 9999999)] public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; }
        public string? Reference { get; set; }
        public string? Notes { get; set; }
    }
}
