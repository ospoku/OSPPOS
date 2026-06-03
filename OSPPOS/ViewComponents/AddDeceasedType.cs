using DMX.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DMX.ViewComponents
{
    public class AddDeceasedType:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(new AddDeceasedTypeVM());
        }
    }
}
