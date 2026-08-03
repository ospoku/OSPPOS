using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Identity;
using DMX.Models;
using OSPPOS.Data;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{
    public class ViewUnits(XContext dContext) : ViewComponent
    {
        public readonly XContext dcx = dContext;
       

        public IViewComponentResult Invoke()
        {
          
            var tList = dcx.Units.Where(a => a.IsDeleted == false).Select(a => new ViewUnitsVM
            {
                PublicId = a.PublicId,
                Name = a.Name,
               Code =a.Code,
               Description = a.Description,
            
            }).ToList();
            return View(tList);
        }
    }
}
