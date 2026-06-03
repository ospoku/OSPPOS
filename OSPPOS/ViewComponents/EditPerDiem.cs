using DMX.Data;
using DMX.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace DMX.ViewComponents
{
    public class EditPerDiem(XContext dContext,IDataProtector protector):ViewComponent
    {
        public readonly XContext ctx=dContext;
        public readonly IDataProtectionProvider provider;
       public  IViewComponentResult Invoke(string Id)
        {
            var perdiemToEdit = ctx.Users.Where(x => x.Id == protector.Unprotect(Id)).Select(x => new EditPerdiemVM { 
                Id=Id,
                Amount= ctx.PerDiems.Where(a => a.UserId == x.Id).Select(a => a.Amount).SingleOrDefault(),
            Department=x.DepartmentId,
            Rank=x.RankId,
            Name=x.Fullname,
            }).FirstOrDefault();

            return View(perdiemToEdit);
        }


    }
}
