using DMX.Data;
using DMX.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DMX.ViewComponents
{
    public class ViewMorgueServices(XContext context):ViewComponent
    {
        public readonly XContext ctx=context;
        public IViewComponentResult Invoke()
        {
            var lList = ctx.MorgueServices.Where(f => f.IsDeleted == false).Select(t => new ViewMorgueServicesVM 
            {
                MorgueServiceId =t.MorgueServiceId,
             Amount=t.Amount,
             ServiceName=t.ServiceName,
            Description=t.Description,
            Code=t.Code,
            }).ToList();
            return View(lList);
        }

    }
}
