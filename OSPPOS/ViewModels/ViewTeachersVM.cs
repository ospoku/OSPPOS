namespace DMX.ViewModels
{
    public class ViewTeachersVM
    {
        public string FacultyId { get; set; }
        public int Capacity { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateofDischarge { get; set; }
        public string Name { get; set; }
        public int ExcuseDays { get; set; }
        public  DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
