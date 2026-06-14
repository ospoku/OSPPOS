using Microsoft.AspNetCore.Mvc;
using OSPPOS.Data;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{
    public class ViewCategories(XContext dContext) : ViewComponent
    {
        public readonly XContext dcx = dContext;
       

        public IViewComponentResult Invoke()
        {
          
            var cList = dcx.Categories.Where(a => a.IsDeleted == false).Select(a => new ViewCategoriesVM
            {
                PublicId = a.PublicId,
                Name = a.Name,
               Code =a.Code,
               Description = a.Description,
               IsActive=a.IsActive,
               
            
            }).ToList();
            return View(cList);
        }
    }
}
