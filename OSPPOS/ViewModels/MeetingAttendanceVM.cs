using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMX.ViewModels
{
    public class MeetingAttendanceVM
    {
        public string EventId { get; set; }
        public string EventName { get; set; }
        public SelectList Attendees { get; set; }
        public DateTime Date {  get; set; }
        public List<string>SelectedParticipants { get; set; }
    }
}
