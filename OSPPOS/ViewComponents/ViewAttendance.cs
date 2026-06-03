using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DMX.Data;
using DMX.Models;
using DMX.ViewModels;

namespace DMX.ViewComponents
{
    public class ViewAttendance(XContext dContext) : ViewComponent
    {
        public readonly XContext dcx = dContext;
       

        public IViewComponentResult Invoke()
        {


            var meetingList = dcx.MeetingAttendance.Where(m => m.IsDeleted == false).Select(m => new ViewAttendanceVM
            {

                MeetingId = m.EventId,
            
                ParticipantId = m.ParticipantId,

                CreatedDate = m.CreatedDate,
            }).OrderByDescending(m => m.CreatedDate).ToList();


            return View(meetingList);
        }
    }
}
