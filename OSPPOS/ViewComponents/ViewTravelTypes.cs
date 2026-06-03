using Microsoft.AspNetCore.Mvc;
using DMX.Data;
using DMX.ViewModels;
using Microsoft.AspNetCore.Identity;
using DMX.Models;

namespace DMX.ViewComponents
{
    public class ViewTravelTypes(XContext dContext) : ViewComponent
    {
        public readonly XContext dcx = dContext;
       

        public IViewComponentResult Invoke()
        {
          
            var tList = dcx.TravelTypes.Where(a => a.IsDeleted == false).Select(a => new ViewTravelTypesVM
            {
                Id = a.TravelTypeId,
                Name = a.Name,
               Code =a.Code,
               Description = a.Description,
            
            }).ToList();
            return View(tList);
        }
    }
}
