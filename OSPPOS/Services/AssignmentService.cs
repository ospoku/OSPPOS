using AspNetCoreHero.ToastNotification.Abstractions;

using Microsoft.AspNetCore.Identity;
using OSPPOS.Data;
using OSPPOS.Models;
using System.Security.Claims;
using static DMX.Constants.Permissions;

namespace OSPPOS.Services
{
    public class AssignmentService
    {
        private readonly UserManager<AppUser> usm;
        private readonly INotyfService notyf;

        private readonly XContext dcx;

        public AssignmentService(UserManager<AppUser> userManager, XContext context, INotyfService notyfService)
        {
            usm = userManager;
            notyf = notyfService;
            dcx = context;
        }

        public async Task<bool> AssignUsers<T>(T model, ClaimsPrincipal userClaims) where T : class
        {
            var userId = usm.GetUserId(userClaims);

            model.GetType().GetProperty("CreatedBy")?.SetValue(model, userId, null);
            model.GetType().GetProperty("CreatedDate")?.SetValue(model, DateTime.UtcNow);
            try
            {
                dcx.Set<T>().Add(model);
                if (await dcx.SaveChangesAsync(userId) > 0)
                { 
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                notyf.Error("An error occurred: " + ex.Message, 5);
                return false;
            }
        }

    }
}

