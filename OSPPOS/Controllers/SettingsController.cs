using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using DMX.Models;
using Microsoft.AspNetCore.Identity;
using DMX.ViewComponents;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


using DMX.Services;
using Microsoft.AspNetCore.Components.Web;
using OSPPOS.Models;
using OSPPOS.Data;
using OSPPOS.Services;


namespace OSPPOS.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class SettingsController(XContext context, INotyfService notyfService, UserManager<AppUser> userManager, EntityService entityService) : Controller
    {
        public readonly XContext dcx = context;
        public readonly INotyfService notyf = notyfService;
        public readonly UserManager<AppUser> usm = userManager;
        public readonly EntityService entityServ = entityService;

   
        public IActionResult SystemSetup()
        {
            return ViewComponent(nameof(SystemSetup));
        }

    }


}







