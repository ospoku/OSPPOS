using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using CsvHelper;
using CsvHelper.Configuration;
using DMX.Data;

using DMX.Models;
using DMX.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Globalization;
namespace DMX.Controllers
{
    public class TrainingController(XContext context, INotyfService notyfService, UserManager<AppUser> userManager, IDataProtectionProvider provider) : Controller
    {
        public readonly XContext dcx = context;
        public readonly INotyfService notyf = notyfService;
        public readonly UserManager<AppUser> usm = userManager;
        public readonly IDataProtector protector = provider.CreateProtector("IdProtector");
        public IActionResult ViewExternalTrainings()
         => ViewComponent(nameof(ViewExternalTrainings));
        public IActionResult ViewParticipants()
      => ViewComponent(nameof(ViewParticipants));


        [HttpGet]
        public IActionResult AddTraining() => ViewComponent(nameof(AddTraining));
       
      

        public IActionResult ViewAttendance()
    => ViewComponent(nameof(ViewAttendance));
        [HttpGet]
        public IActionResult ViewTrainings()
    => ViewComponent(nameof(ViewTrainings));
        [HttpPost]
        public async Task<IActionResult> AddTraining(AddTrainingVM addTrainingVM)
        {
            Training addThisTraining = new()
            {
                EventName = addTrainingVM.WorkshopTitle,
                Date = addTrainingVM.TrainingDate,
                Description = addTrainingVM.Description,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = usm.GetUserAsync(User).Result?.UserName,
            };

            dcx.Trainings.Add(addThisTraining);
            await dcx.SaveChangesAsync();

            return RedirectToAction("ViewInternalTrainings");
        }
        [HttpGet]
        public IActionResult MeetingAttendance(string Id) => ViewComponent(nameof(MeetingAttendance), Id);
        [HttpPost]
        public async Task<IActionResult> MeetingAttendance(string Id, MeetingAttendanceVM attVM)
        {
            //    if (ModelState.IsValid)
            //    {

            foreach (var attendee in attVM.SelectedParticipants)
            {
                MeetingAttendance addThisAttendnace = new()
                {
                    CreatedDate = DateTime.UtcNow,
                    ParticipantId = attendee,
                    EventId = protector.Unprotect(Id),

                    CreatedBy = usm.GetUserAsync(User).Result.UserName,

                };

                dcx.MeetingAttendance.Add(addThisAttendnace);
            }

               
                if (await dcx.SaveChangesAsync(usm.GetUserAsync(User).Result.UserName) > 0)
                {
                    notyf.Success("Client successfully created.");
                    return RedirectToAction("ViewMeetings");
                }
                else
                {
                    notyf.Error("Member creation error!!! Please try again");




                    return ViewComponent(nameof(MeetingAttendance));
                }



            }
        
       



         
        


        [HttpGet]
        public IActionResult AddMeeting()
        {
            return ViewComponent(nameof(AddMeeting));
        }

        [HttpPost]
        public async Task< IActionResult> AddMeeting(AddMeetingVM addMeetingVM)
        {
            Meeting addThisMeeting = new()
            {
                Name = addMeetingVM.Name,
                Description = addMeetingVM.Description,
                CreatedDate = DateTime.Now,
                CreatedBy = usm.GetUserAsync(User).Result?.UserName,
                Date = addMeetingVM.Date,
            };
            dcx.Meetings.Add(addThisMeeting);

            if (await dcx.SaveChangesAsync( usm.GetUserAsync(User).Result?.UserName) > 0)
            {
                notyf.Success("Client successfully created.");
                return RedirectToAction("ViewMeetings");

            }
            else
            {
                notyf.Error("Member creation error!!! Please try again");

                return RedirectToAction("AddMeeting");
            }


       
        }

        [HttpGet]
        public IActionResult ViewMeetings()
        {
            return ViewComponent(nameof(ViewMeetings));
        }

        

        //[HttpGet]
        //public IActionResult ImportFromStaffList()
        //{
        //    return ViewComponent(nameof(ImportFromStaffList");
        //}
        //[HttpPost]
        //public async Task<IActionResult> ImportFromStaffList(ImportFromStaffListVM importFromStaffListVM)
        //{

        //    foreach (var staff in importFromStaffListVM.SelectedId)
        //    {

        //        var selectedStaff = dcx.StaffList.Where(x => x.StaffId.Contains(staff.ToString())).ToList();

        //        foreach (var staffid in selectedStaff)
        //        {
        //            Participant newParticipant = new()
        //            {
        //                DateOfBirth = staffid.DateOfBirth,
        //                Contact = staffid.Contact,
        //                Name = staffid.Name,
        //                Rank = staffid.Rank,
        //                Department = staffid.Department,
        //            };

        //            dcx.Participants.Add(newParticipant);
        //            await dcx.SaveChangesAsync();
        //        }

        //    };



        //    return ViewComponent(nameof(ViewParticipants");
        //}
    //    [HttpGet]
    //    public IActionResult ImportFromExcel()
    //    {
    //        return ViewComponent(nameof(ImportFromExcel");
    //    }
    //    [HttpPost]
    //    public async Task<IActionResult> ImportFromExcel(IFormFile formFile)
    //    {
    //        var data = new MemoryStream();
    //        await formFile.CopyToAsync(data);

    //        data.Position = 0;
    //        using (var reader = new StreamReader(data))
    //        {
    //            var bad = new List<string>();
    //            var conf = new CsvConfiguration(CultureInfo.InvariantCulture)
    //            {
    //                HasHeaderRecord = true,
    //                HeaderValidated = null,
    //                MissingFieldFound = null,
    //                DetectColumnCountChanges = true,
    //                InjectionOptions= InjectionOptions.Exception,

    //                BadDataFound = context =>
    //                {
    //                    bad.Add(context.RawRecord);
    //                }
    //            };
    //            using (var csvReader = new CsvReader(reader, conf))
    //            {
    //                while (csvReader.Read())
    //                {
    //                    var Name = csvReader.GetField(0).ToString();
    //                    var Contact = csvReader.GetField(1).ToString();
    //                    var DoB = csvReader.GetField(2);
    //                    var Department = csvReader.GetField(3).ToString();

    //                    //await dcx.Participants.AddAsync(new Participant
    //                    //{
    //                    //    Name = Name.ToString(),
    //                    //    Contact = Contact,
    //                    //    Department = Department,
    //                    //    DateOfBirth = DateTime.Parse(DoB),

    //                    //});
    //                    dcx.SaveChanges();
    //                }
    //            };
    //        }
    //        return ViewComponent(nameof(ViewParticipants");
    //    }
    }
}



