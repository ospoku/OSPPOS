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
                if (!dcx.Categories.Any())
                {
                    dcx.Categories.AddRange(
                        new Category { Name = "Electronics", Code = "ELE",Description="" },
                        new Category { Name = "Accessories", Code = "ACC", Description = "" },
                        new Category { Name = "Services", Code = "SRV", Description = "" },
                        new Category { Name = "Groceries", Code = "GRC", Description = "" }
                    );
                    dcx.SaveChanges();
                }

                // =========================
                // UNITS
                // =========================
                if (!dcx.Units.Any())
                {
                    dcx.Units.AddRange(
                        new Unit { Name = "Piece", Code = "pcs" },
                        new Unit { Name = "Kilogram", Code = "kg" },
                        new Unit { Name = "Liter", Code = "L" }
                    );
                    dcx.SaveChanges();
                }

                // =========================
                // SUPPLIERS
                // =========================
                if (!dcx.Suppliers.Any())
                {
                    dcx.Suppliers.AddRange(
                        new Supplier { Name = "TechSource Ltd" },
                        new Supplier { Name = "Ghana Wholesale Traders" },
                        new Supplier { Name = "FreshMart Supplies" }
                    );
                    dcx.SaveChanges();
                }

                // =========================
                // CUSTOMERS
                // =========================
                if (!dcx.Customers.Any())
                {
                    dcx.Customers.AddRange(
                        new Customer { Name = "Walk-in Customer" },
                        new Customer { Name = "Kwame Mensah" },
                        new Customer { Name = "Ama Owusu" },
                        new Customer { Name = "Kofi Asare" }
                    );
                    dcx.SaveChanges();
                }

                // =========================
                // PRODUCTS
                // =========================
                if (!dcx.Products.Any())
                {
                    var cat = dcx.Categories.ToList();
                    var units = dcx.Units.ToList();
                    var suppliers = dcx.Suppliers.ToList();

                    dcx.Products.AddRange(
                        new Product { Name = "Samsung Phone", SellingPrice = 1200, CategoryId = cat[0].CategoryId, UnitId = units[0].UnitId, SupplierId = suppliers[0].SupplierId },
                        new Product { Name = "Laptop", SellingPrice = 2500, CategoryId = cat[0].CategoryId, UnitId = units[0].UnitId, SupplierId = suppliers[0].SupplierId },
                        new Product { Name = "Charger", SellingPrice = 50, CategoryId = cat[1].CategoryId, UnitId = units[0].UnitId, SupplierId = suppliers[1].SupplierId },
                        new Product { Name = "Earphones", SellingPrice = 80, CategoryId = cat[1].CategoryId, UnitId = units[0].UnitId, SupplierId = suppliers[1].SupplierId },
                        new Product { Name = "Repair Service", SellingPrice = 150, CategoryId = cat[2].CategoryId, UnitId = units[0].UnitId, SupplierId = suppliers[0].SupplierId },
                        new Product { Name = "Rice", SellingPrice = 10, CategoryId = cat[3].CategoryId, UnitId = units[1].UnitId, SupplierId = suppliers[2].SupplierId },
                        new Product { Name = "Cooking Oil", SellingPrice = 15, CategoryId = cat[3].CategoryId, UnitId = units[2].UnitId, SupplierId = suppliers[2].SupplierId }
                    );

                    dcx.SaveChanges();
                }
            // =========================
            // 📦 STOCK BATCHES (GRN)
            // =========================
        
            if (!dcx.PaymentMethods.Any())
            {
                dcx.PaymentMethods.AddRange(
                    new PaymentMethod { Name = "Cash", Code = "CASH" },
                    new PaymentMethod { Name = "Mobile Money", Code = "MOMO" },
                    new PaymentMethod { Name = "Bank Transfer", Code = "BANK" },
                    new PaymentMethod { Name = "Card", Code = "CARD" },
                    new PaymentMethod { Name = "Cheque", Code = "CHQ" }
                );

                dcx.SaveChanges();
            }

            // =========================
            // LOAD DATA
            // =========================
            var products = dcx.Products.ToList();
                var customers = dcx.Customers.ToList();

                // =========================
                // INVOICES
                // =========================
                if (!dcx.Invoices.Any())
                {
                    var invoices = new List<Invoice>();

                    for (int i = 1; i <= 20; i++)
                    {
                        var items = new List<InvoiceItem>();

                        for (int j = 0; j < rand.Next(1, 4); j++)
                        {
                            var p = products[rand.Next(products.Count)];

                            items.Add(new InvoiceItem
                            {
                                ProductId = p.ProductId,
                                Quantity = rand.Next(1, 5),
                                UnitPrice = p.SellingPrice
                            });
                        }

                        var invoice = new Invoice
                        {
                            InvoiceNumber = $"INV-{i:D3}",
                            InvoiceDate = DateTime.UtcNow.AddDays(-rand.Next(30)),
                            DueDate = DateTime.UtcNow.AddDays(rand.Next(5, 20)),
                            CustomerId = rand.Next(0, 2) == 1 ? customers[rand.Next(customers.Count)].CustomerId : null,
                            WalkInCustomerName = $"Walk-in {i}",
                            Discount = rand.Next(0, 20),
                       
                            Items = items
                        };

                        if (rand.Next(0, 2) == 1)
                        {
                            var total = items.Sum(x => x.Quantity * x.UnitPrice);

                        invoice.Payments = new List<Payment>
                        {
                            new Payment
                            {
                                Amount = total * 0.5m,
                                PaymentDate = DateTime.UtcNow,
                                PaymentMethodId=1
                            }
                        };
                        }

                        invoices.Add(invoice);
                    }

                    dcx.Invoices.AddRange(invoices);
                    dcx.SaveChanges();
                }

                // =========================
                // SALE ORDERS
                // =========================
                if (!dcx.SaleOrders.Any())
                {
                    var orders = new List<SaleOrder>();

                    for (int i = 1; i <= 20; i++)
                    {
                        var items = new List<SaleOrderItem>();

                        for (int j = 0; j < rand.Next(1, 4); j++)
                        {
                            var p = products[rand.Next(products.Count)];

                            items.Add(new SaleOrderItem
                            {
                                ProductId = p.ProductId,
                                Quantity = rand.Next(1, 5),
                                UnitPrice = p.SellingPrice
                            });
                        }

                        orders.Add(new SaleOrder
                        {
                            OrderNumber = $"SO-{i:D3}",
                            OrderDate = DateTime.UtcNow.AddDays(-rand.Next(30)),
                            CustomerId = customers[rand.Next(customers.Count)].CustomerId,
                            Items = items
                        });
                    }

                    dcx.SaleOrders.AddRange(orders);
                    dcx.SaveChanges();
                }

            if (!dcx.StockBatches.Any())
            {


                var suppliers = dcx.Suppliers.ToList();
            

                var batches = new List<StockBatch>();

                for (int i = 1; i <= 15; i++)
                {
                    var supplier = suppliers[rand.Next(suppliers.Count)];

                    var items = new List<StockBatchItem>();

                    int itemCount = rand.Next(2, 5);

                    for (int j = 0; j < itemCount; j++)
                    {
                        var product = products[rand.Next(products.Count)];

                        var quantity = rand.Next(5, 50);
                        var costPrice = product.SellingPrice * 0.6m; // assume margin

                        items.Add(new StockBatchItem
                        {
                            ProductId = product.ProductId,
                            Quantity = quantity,
                            UnitCost = costPrice
                        });
                    }

                    batches.Add(new StockBatch
                    {
                        GRNNumber = $"GRN-{DateTime.UtcNow.Year}-{i:D3}",
                        SupplierId = supplier.SupplierId,
                        ReceivedDate = DateTime.UtcNow.AddDays(-rand.Next(1, 60)),
                        SupplierInvoiceRef = $"SUP-INV-{rand.Next(1000, 9999)}",
                        Notes = "Initial stock load",
                        Items = items
                    });
                }

                dcx.StockBatches.AddRange(batches);
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






