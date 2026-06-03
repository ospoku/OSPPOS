using DMX.Data;
using DMX.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DMX.ViewComponents
{
    public class ViewCashLimit(XContext context):ViewComponent
    {
        public readonly XContext ctx=context;
        public IViewComponentResult Invoke()
        {

            var pettyCashlimits=ctx.CashLimits.Select(p=>new ViewCashLimitVM
            {
                CashLimitId=p.CashLimitId,
              
                Amount=p.Amount,
          
            }).ToList();

            return View(pettyCashlimits);
        }
    }
}
