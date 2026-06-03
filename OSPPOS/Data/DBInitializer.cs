using DMX.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DMX.Data
{
    public class DBInitializer(XContext dContext, RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
    {
        public readonly XContext dcx = dContext;
        public readonly RoleManager<AppRole> rol = roleManager;
        public readonly UserManager<AppUser> usm = userManager;
        public async Task Initialize()
        {
            dcx.Database.EnsureCreated();
            if (!rol.Roles.Any())
            {
                await rol.CreateAsync(new AppRole() { Name = "Basic", Rolename = "Basic", Description = "Role for basic users" });
                await rol.CreateAsync(new AppRole() { Name = "Manager", Rolename = "Manager", Description = "Role for managers" });
                await rol.CreateAsync(new AppRole() { Name = "SuperAdmin", Rolename = "SuperAdmin", Description = "Role for superadmin" });
                await rol.CreateAsync(new AppRole() { Name = "Admin", Rolename = "Admin", Description = "Role for admin users" });
            }

            if (!dcx.DeceasedTypes.Any())
            {
                dcx.DeceasedTypes.Add(new DeceasedType()
                {
                    Name = "Brought In Dead",
                    Code = "BID",
                    Description = "Name for a Patient who was broguth in Dead"
                });
                dcx.DeceasedTypes.Add(new DeceasedType()
                {
                    Name = "Dead In Ward",
                    Code = "DIW",
                    Description = "Name for a Patient who Died in Ward"
                });
            }
            if (!dcx.Statuses.Any())
            {
                dcx.Statuses.AddRange(new Status
                {
                    Name = "Open",
                    Code = "OPN",
                    Description = "Name for a Patient who was broguth in Dead"
                },
                new Status
                {

                    Name = "Close",
                    Code = "CLS",
                    Description = "Name for a Patient who Died in Ward"
                }, new Status
                {
                    Name = "Pending",
                    Code = "PND",
                    Description = "Name for a Patient who Died in Ward"
                }
                , new Status {
                    Name = "Archvied", Code = "ARC",
                    Description = "Name for a Patient who Died in Ward"
                }, new Status { Name = "In Progress",
                    Code = "INP",
                    Description = "Name for a Patient who Died in Ward"
                }
                );
            }
            if (!dcx.Categories.Any())
            {
                dcx.Categories.AddRange(new Category
                {
                    Name = "Repairs",
                    Code = "BID",
                    Description = "Name for a Patient who was broguth in Dead"
                },
                new Category
                {
                    Name = "Maintenance",
                    Code = "DIW",
                    Description = "Name for a Patient who Died in Ward"
                },
                new Category
                {
                    Name = "Replacement",
                    Code = "DIW",
                    Description = "Name for a Patient who Died in Ward"
                });
            }
                
            if (!dcx.Priorities.Any())
            {
                dcx.Priorities.Add(new Priority()
                {
                    Name = "High",
                    Code = "H",
                    Description = "Name for a Patient who was broguth in Dead"
                });
                dcx.Priorities.Add(new Priority()
                {
                    Name = "Low",
                    Code = "L",
                    Description = "Name for a Patient who Died in Ward"
                });
                dcx.Priorities.Add(new Priority()
                {
                    Name = "Medium",
                    Code = "M",
                    Description = "Name for a Patient who Died in Ward"
                });
                dcx.Priorities.Add(new Priority()
                {
                    Name = "Emergency",
                    Code = "E",
                    Description = "Name for a Patient who Died in Ward"
                });
            }
            if (!dcx.RequestTypes.Any())
            {
                dcx.RequestTypes.Add(new RequestType()
                {
                    Name = "Estate",
                    Code = "EST",
                    Description = "Name for a Patient who was broguth in Dead"
                });
                dcx.RequestTypes.Add(new RequestType()
                {
                    Name = "ICT",
                    Code = "IT",
                    Description = "Name for a Patient who Died in Ward"
                });
           
            }
            if (!dcx.TravelTypes.Any())
            {
                dcx.TravelTypes.Add(new TravelType()
                {
                    Name = "Conference",
                    Code = "CNF",
                    Description = "Name for a Patient who was broguth in Dead"
                });
                dcx.TravelTypes.Add(new TravelType()
                {
                    Name = "Seminar",
                    Code = "SMR",
                    Description = "Name for a Patient who Died in Ward"
                });

            }
            if (!dcx.MorgueServices.Any())
            {
                dcx.MorgueServices.Add(new MorgueService ()
                {
                    ServiceName = "Formalin",
                    Code="FML",
                   Description="xxxxxxxxxx",
                });
                dcx.MorgueServices.Add(new MorgueService()
                {
                    ServiceName = "Embalming",
                   Code="EBM",
                   Description="xxxxxxxxxxx",
                });

            }

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






