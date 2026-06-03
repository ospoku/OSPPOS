namespace DMX.ViewModels
{
    public class ViewExcuseDutiesVM
    {

        public Guid PublicId { get; set; }
        public string Sender { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateofDischarge { get; set; }
        public string OperationDiagnosis { get; set; }
        public int ExcuseDays { get; set; }
        public  DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
