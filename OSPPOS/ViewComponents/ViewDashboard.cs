using Microsoft.AspNetCore.Mvc;
using DMX.Data;
using DMX.ViewModels;

namespace DMX.ViewComponents
{
    public class ViewDashboard(XContext context) : ViewComponent
    {
        public readonly XContext ctx = context;

        public IViewComponentResult Invoke()

        {
            ViewDashboardVM viewDashboardVM = new()
            {
                TotalDocuments = ctx.Letters.Where(a => a.IsDeleted == false).Count().ToString(),
                //TotalFemales=prx.Documents.Where(a=>a.IsDeleted==false&a.Gender.GenderName=="Female").Count().ToString(),
                //TotalMales = prx.Documents.Where(a => a.IsDeleted == false & a.Gender.GenderName == "Male").Count().ToString(),
           PettyCash=((int)ctx.PettyCash.Sum(x=>x.Amount)),
            };
        return View(viewDashboardVM);
           
        }
    }
}
