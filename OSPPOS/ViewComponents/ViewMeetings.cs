using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DMX.Data;
using DMX.Models;
using DMX.ViewModels;

namespace DMX.ViewComponents
{
    public class ViewMeetings(XContext dContext) : ViewComponent
    {
        public readonly XContext dcx = dContext;
       

        public IViewComponentResult Invoke()
        {


            var meetingList = dcx.Meetings.Where(m => m.IsDeleted == false).Select(m => new ViewMeetingsVM
            {

                MeetingId = m.MeetingId,
                Description = m.Description,
                Name = m.Name,




                CreatedDate = m.CreatedDate,
            }).OrderByDescending(m => m.CreatedDate).ToList();


            return View(meetingList);
        }
    }
}
