using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DMX.ViewComponents
{
    public class UserAlert:ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
