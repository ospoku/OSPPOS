using DMX.Models;
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
            dcx.Database.EnsureCreated();
            if (!rol.Roles.Any())
            {
                await rol.CreateAsync(new AppRole() { Name = "Basic", Rolename = "Basic", Description = "Role for basic users" });
                await rol.CreateAsync(new AppRole() { Name = "Manager", Rolename = "Manager", Description = "Role for managers" });
                await rol.CreateAsync(new AppRole() { Name = "SuperAdmin", Rolename = "SuperAdmin", Description = "Role for superadmin" });
                await rol.CreateAsync(new AppRole() { Name = "Admin", Rolename = "Admin", Description = "Role for admin users" });
            }

            if (!dcx.Categories.Any())
            {
                dcx.Categories.AddRange(
                    new Category { Name = "Electronics", Code = "ELE", Description = "Electronic devices" },
                    new Category { Name = "Accessories", Code = "ACC", Description = "Device accessories" },
                    new Category { Name = "Services", Code = "SRV", Description = "Service offerings" },
                    new Category { Name = "Groceries", Code = "GRC", Description = "Food and essentials" }
                );

                dcx.SaveChanges();
            }

            // --------------------
            // UNITS
            // --------------------
            if (!dcx.Units.Any())
            {
                dcx.Units.AddRange(
                    new Unit { Name = "Piece", Code = "pcs" },
                    new Unit { Name = "Kilogram", Code = "kg" },
                    new Unit { Name = "Liter", Code = "L" }
                );

                dcx.SaveChanges();
            }

            // --------------------
            // SUPPLIERS
            // --------------------
            if (!dcx.Suppliers.Any())
            {
                dcx.Suppliers.AddRange(
                    new Supplier { Name = "TechSource Ltd", Phone = "0240000001", Email = "sales@techsource.com" },
                    new Supplier { Name = "Ghana Wholesale Traders", Phone = "0240000002", Email = "info@ghanawholesale.com" },
                    new Supplier { Name = "FreshMart Supplies", Phone = "0240000003", Email = "contact@freshmart.com" }
                );

                dcx.SaveChanges();
            }

            // --------------------
            // CUSTOMERS
            // --------------------
            if (!dcx.Customers.Any())
            {
                dcx.Customers.AddRange(
                    new Customer { Name = "Walk-in Customer", Phone = "0000000000" },
                    new Customer { Name = "Kwame Mensah", Phone = "0241111111", Email = "kwame.mensah@email.com" },
                    new Customer { Name = "Ama Owusu", Phone = "0242222222", Email = "ama.owusu@email.com" },
                    new Customer { Name = "Kofi Asare", Phone = "0243333333", Email = "kofi.asare@email.com" }
                );

                dcx.SaveChanges();
            }

            // --------------------
            // PRODUCTS (FIXED PROPERLY)
            // --------------------
            if (!dcx.Products.Any())
            {
                var electronics = dcx.Categories.First(c => c.Code == "ELE");
                var accessories = dcx.Categories.First(c => c.Code == "ACC");
                var services = dcx.Categories.First(c => c.Code == "SRV");
                var groceries = dcx.Categories.First(c => c.Code == "GRC");

                var pcs = dcx.Units.First(u => u.Code == "pcs");
                var kg = dcx.Units.First(u => u.Code == "kg");
                var liter = dcx.Units.First(u => u.Code == "L");

                var techSupplier = dcx.Suppliers.First(s => s.Name == "TechSource Ltd");
                var wholesaleSupplier = dcx.Suppliers.First(s => s.Name == "Ghana Wholesale Traders");
                var grocerySupplier = dcx.Suppliers.First(s => s.Name == "FreshMart Supplies");

                dcx.Products.AddRange(

                    // 1. Electronics (2 products)
                    new Product
                    {
                        Name = "Samsung Smartphone",
                        SellingPrice = 1200m,
                        CategoryId = electronics.CategoryId,
                        UnitId = pcs.UnitId,
                        SupplierId = techSupplier.SupplierId
                    },
                    new Product
                    {
                        Name = "Laptop",
                        SellingPrice = 2500m,
                        CategoryId = electronics.CategoryId,
                        UnitId = pcs.UnitId,
                        SupplierId = techSupplier.SupplierId
                    },

                    // 2. Accessories (2 products)
                    new Product
                    {
                        Name = "Phone Charger",
                        SellingPrice = 50m,
                        CategoryId = accessories.CategoryId,
                        UnitId = pcs.UnitId,
                        SupplierId = wholesaleSupplier.SupplierId 
                    },
                    new Product
                    {
                        Name = "Earphones",
                        SellingPrice = 80m,
                        CategoryId = accessories.CategoryId,
                        UnitId = pcs.UnitId,
                        SupplierId = wholesaleSupplier.SupplierId
                    },

                    // 3. Services (1 product)
                    new Product
                    {
                        Name = "Phone Repair",
                        SellingPrice = 150m,
                        CategoryId = services.CategoryId,
                        UnitId = pcs.UnitId,
                        SupplierId = techSupplier.SupplierId
                    },

                    // 4. Groceries (2 products)
                    new Product
                    {
                        Name = "Rice",
                        SellingPrice = 10m,
                        CategoryId = groceries.CategoryId,
                        UnitId = kg.UnitId,
                        SupplierId = grocerySupplier.SupplierId
                    },
                    new Product
                    {
                        Name = "Cooking Oil",
                        SellingPrice = 15m,
                        CategoryId = groceries.CategoryId,
                        UnitId = liter.UnitId,
                        SupplierId = grocerySupplier.SupplierId
                    }
                );

                dcx.SaveChanges();
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






