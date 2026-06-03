using DMX.Data;
using DMX.Models;
using DMX.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DMX.ViewComponents
{
    
    public class EditLimit(XContext dContext, UserManager<AppUser> userManager, IDataProtectionProvider provider) : ViewComponent
    {
        public readonly XContext dcx = dContext;
        public readonly UserManager<AppUser> usm = userManager;
        public readonly IDataProtector protector=provider.CreateProtector("IdProtector");
        public IViewComponentResult Invoke(string Id)
        {

              
                CashLimit limitToEdit = new();
                limitToEdit = (from p in dcx.CashLimits where p.CashLimitId == @protector.Unprotect(Id) select p).FirstOrDefault();

                EditLimitVM editLimitVM = new()
                {

              
                
                    Amount = (from x in dcx.CashLimits where x.CashLimitId == @protector.Unprotect(Id) select x.Amount).FirstOrDefault(),
                   
                };


                return View(editLimitVM);
            }
          

        }
    }


