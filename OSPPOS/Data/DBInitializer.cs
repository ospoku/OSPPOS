using DMX.Models;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Models;
using System;
using System.Security.Claims;

namespace OSPPOS.Data
{
    public class DBInitializer(XContext dContext, RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
    {
        public readonly XContext dcx = dContext;
        public readonly RoleManager<AppRole> rol = roleManager;
        public readonly UserManager<AppUser> usm = userManager;

  

   

            public async Task Initialize()
            {
                var rand = new Random();

                dcx.Database.Migrate();

                // =========================
                // ROLES
                // =========================
                if (!rol.Roles.Any())
                {
                    await rol.CreateAsync(new AppRole { Name = "Basic", Rolename = "Basic",Description="Role for Basic Users" });
                    await rol.CreateAsync(new AppRole { Name = "Manager", Rolename = "Manager" ,Description="Role for Managers"});
                    await rol.CreateAsync(new AppRole { Name = "Admin", Rolename = "Admin",Description="Role for Admin" });
                    await rol.CreateAsync(new AppRole { Name = "SuperAdmin", Rolename = "SuperAdmin" , Description="Role for SuperAdmin"});
                }

                // =========================
                // CATEGORIES
                // =========================
      

                // =========================
                // UNITS
                // =========================
    

                // =========================
                // SUPPLIERS
                // =========================
        

                // =========================
                // CUSTOMERS
                // =========================
         

                // =========================
                // PRODUCTS
                // =========================
   
            // =========================
            // 📦 STOCK BATCHES (GRN)
            // =========================
        


            // =========================
            // LOAD DATA
            // =========================


                // =========================
                // INVOICES
                // =========================
 

                // =========================
                // SALE ORDERS
                // =========================
   





            List<Claim> claimlist =
            [

                new(ClaimTypes.Name,"SuperAdmin"),
                new Claim(ClaimTypes.Email,"ospoku@gmail.com"),
                new Claim(ClaimTypes.Role,"SuperAdmin"),
                


            ];

            List<Claim> claimlist2 =
                 [

                     new Claim(ClaimTypes.Name,"Admin"),
                new Claim(ClaimTypes.Email,"kofipoku84@gmail.com"),
                new Claim(ClaimTypes.Role,"Admin"),


            ];
            IdentityResult identityResult;


            if (await usm.FindByNameAsync("SuperAdmin") == null)
            {
                AppUser superUser = new()
                {
                    UserName = "SuperAdmin",
                    Lastname = "SuperAdmin",
                    Firstname = "SuperAdmin",
                    Email = "ospoku@gmail.com",
                    PhoneNumber = "0244139692",
                    EmailConfirmed = true,
                    DepartmentId = "xxxxxxxxxxxxxxxxx",
                    RankId = "xxxxxxxxxxxxxxxxxx",
                };

                identityResult = await usm.CreateAsync(superUser, "OSP@SuperAdmin12345");
                if (identityResult.Succeeded)
                {
                    await usm.AddToRoleAsync(superUser, "SuperAdmin");
                    await usm.AddClaimsAsync(superUser, claimlist);
                    await usm.AddClaimAsync(superUser, new Claim(ClaimTypes.NameIdentifier,superUser.Id));
                };

            };
            if (await usm.FindByNameAsync("Admin") == null)
            {
                AppUser superUser = new()
                {
                    UserName = "Admin",
                    Lastname = "Admin",
                    Firstname = "Admin",
                    Email = "kofipoku84@gmail.com",
                    PhoneNumber = "0244139692",
                    EmailConfirmed = true,
                    DepartmentId = "xxxxxxxxxxxxxxxxx",
                    RankId = "xxxxxxxxxxxxxxxxxx",
                };

                identityResult = await usm.CreateAsync(superUser, "OSP@Admin12345");
                if (identityResult.Succeeded)
                {
                    await usm.AddToRoleAsync(superUser, "Admin");
                    await usm.AddClaimsAsync(superUser, claimlist2);
                };

            };

        }
    }
}






