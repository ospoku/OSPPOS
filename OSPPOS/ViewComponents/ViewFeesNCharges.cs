using DMX.Data;
using DMX.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DMX.ViewComponents
{
    public class ViewFeesNCharges(XContext context):ViewComponent
    {
        public readonly XContext ctx=context;
        public IViewComponentResult Invoke()
        {
            var lList = ctx.FeeStructures.Where(f => f.IsDeleted == false).Select(t => new ViewFeeStructuresVM 
            {
                DeceasedType=t.DeceasedType.Name,
               Fee=t.Fee,
               Name=t.Name,
               Max=t.MaxDays,
               Min=t.MinDays,
               PublicId=t.PublicId  ,
            }).ToList();
            
    


            return View(lList);
        }

    }
}
