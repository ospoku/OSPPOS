namespace DMX.ViewModels
{
    public class ViewFeeStructuresVM
    {
        public Guid PublicId { get; set; }
        public string Name { get; set; }   
        public string DeceasedType { get; set; }
        public int Min {  get; set; }
        public int Max { get; set; }
        public decimal Fee { get; set; }
    }
}
