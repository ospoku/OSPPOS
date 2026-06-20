namespace OSPPOS.Models
{
    public class PaymentStatus
    {
        public int PaymentStatusId { get; set; }
        public string PaymentStatusName { get; set; }
        public string PaymentStatusDescription { get; set; }
        public string Code { get; set; }
        public ICollection<Payment> Payments {  get; set; }
    }
}
