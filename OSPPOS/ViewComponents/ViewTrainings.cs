using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DMX.Data;
using DMX.Models;
using DMX.ViewModels;

namespace DMX.ViewComponents
{
    public class ViewTrainings(XContext dContext, UserManager<AppUser> userManager) : ViewComponent
    {
        public readonly XContext dcx = dContext;
        public readonly UserManager<AppUser> usm = userManager;

        public IViewComponentResult Invoke()
        {


            var trainingList = dcx.Trainings.Where(d => d.IsDeleted == false).Select(d => new ViewTrainingsVM
            {

               TrainingId = d.TrainingId,
                Date = d.Date,
              Description = d.Description,  
               
               EventName = d.EventName,


                CreatedDate = d.CreatedDate,
            }).OrderByDescending(t => t.CreatedDate).ToList();


            return View(trainingList);
        }
    }
}
