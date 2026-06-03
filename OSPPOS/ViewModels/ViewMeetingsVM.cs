namespace DMX.ViewModels
{
    public class ViewMeetingsVM
    {
        public string Name { get; set; }
        public string Description { get;  set; }
        public DateTime Date { get; set; }
   public string MeetingId { get; set; }
        public DateTime? CreatedDate { get; internal set; }
    }
}
