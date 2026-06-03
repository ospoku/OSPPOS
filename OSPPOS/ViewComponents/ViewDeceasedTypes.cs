using Microsoft.AspNetCore.Mvc;
using DMX.Data;
using DMX.ViewModels;
using Microsoft.AspNetCore.Identity;
using DMX.Models;

namespace DMX.ViewComponents
{
    public class ViewDeceasedTypes(XContext dContext) : ViewComponent
    {
        public readonly XContext dcx = dContext;

       

        public IViewComponentResult Invoke()
        {
            
            var dTypes = dcx.DeceasedTypes.Where(a => a.IsDeleted == false).Select(a => new ViewDTypesVM
            {
                TypeId = a.PublicId,
                Code = a.Code,
                Name= a.Name,
            Description = a.Description,
            
            }).ToList();
            return View(dTypes);
        }
    }
}
