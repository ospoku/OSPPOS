namespace DMX.ViewModels
{
    public class ViewPettyCashVM
    {
        public Guid PettyCashId { get; set; }
     
        public string   Sender { get; set; }
        
        public string Purpose {  get; set; }
        public decimal Amount {  get; set; }
        public string ReferenceNumber { get;set; }
        public DateTime? CreatedDate { get; internal set; }
    }
}
