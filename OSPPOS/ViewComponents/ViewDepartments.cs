using DMX.Data;
using DMX.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DMX.ViewComponents
{
    public class ViewDepartments(XContext context):ViewComponent

    {
        public readonly XContext ctx = context;
        public IViewComponentResult Invoke()
        {
            var dept = ctx.Departments.Select(x=>new ViewDepartmentsVM {
            
            Id=x.PublicId,
            Description=x.Description,
            Name=x.Name,

            }) .ToList(); 

            return View(dept);
        }
    }
}
