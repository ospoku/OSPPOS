using DMX.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DMX.ViewComponents
{
    public class AddDepartment:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var departmentVM = new AddDepartmentVM()
            {

            };


            return View(departmentVM);
        }
    }
}
