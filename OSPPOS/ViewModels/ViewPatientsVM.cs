namespace DMX.ViewModels
{
    public class ViewPatientsVM
    {
    

        public Guid PatientId { get; set; }
        public DateTime Date { get; set; }
        public string PatientName { get; set; }
        public decimal OtherFees { get; set; }
        public string WardInCharge { get; set; }
        public string FolderNo { get; set; }
        public string FinalDiagnoses { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string TagNo { get; set; }
        public string DeceasedTypeId { get; set; }
    }
}
