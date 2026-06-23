
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Data;
using OSPPOS.Interfaces;
using OSPPOS.Models;
using OSPPOS.ViewModels;
using System;
using System.Security.Claims;
namespace OSPPOS.Services
{
    public class SaleService(XContext ctx) : ISaleService
        {
        public async Task<(bool Success, string Error, SaleOrder? Order)> AddSaleAsync(AddSaleVM addSaleVM, string userId)
        {
            try
            {
                var productIds = addSaleVM.Items.Select(i => i.ProductId).ToList();

                var products = await ctx.Products
                    .Where(p => productIds.Contains(p.ProductId))
                    .ToDictionaryAsync(p => p.ProductId);
                if (addSaleVM.CustomerId == null && string.IsNullOrWhiteSpace(addSaleVM.WalkInCustomerName))
                {
                    addSaleVM.WalkInCustomerName = null!;
                    return (false, "Customer is required", null);

                }

                var order = new SaleOrder
                {
                    OrderNumber = await GenerateOrderNumberAsync(),

                    CustomerId = addSaleVM.CustomerId,
                    WalkInCustomerName = addSaleVM.WalkInCustomerName,
                    Notes = addSaleVM.Notes,
                    Discount = addSaleVM.Discount,
                    DiscountPercent = addSaleVM.DiscountPercent,
                    DueDate = addSaleVM.DueDate,
                    CreatedBy = userId,
                };

                if (addSaleVM.CustomerId != null)
                {
                    order.CustomerId = addSaleVM.CustomerId;
                    order.WalkInCustomerName = null;
                }
                else
                {
                    order.CustomerId = null;
                    order.WalkInCustomerName = addSaleVM.WalkInCustomerName;
                }
                foreach (var item in addSaleVM.Items)
                {
                    if (!products.TryGetValue(item.ProductId, out var product))
                    {
                        return (false, $"Product not found: {item.ProductId}", null);
                    }

                    if (product.CurrentStock < item.Quantity)
                    {
                        return (false, $"Insufficient stock for {product.Name}", null);
                    }

                    order.Items.Add(new SaleOrderItem
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    });

                    product.CurrentStock -= item.Quantity;

                    ctx.StockMovements.Add(new StockMovement
                    {
                        ProductId = item.ProductId,
                        Quantity = -item.Quantity,
                        Order = order,
                        Type = "SALE"
                    });
                }

                ctx.SaleOrders.Add(order);

                if (addSaleVM.InitialPayment > 0)
                {
                    ctx.Payments.Add(new Payment
                    {
                        Amount = addSaleVM.InitialPayment,
                        Reference = addSaleVM.PaymentReference,
                        Order = order
                    });
                }
                foreach (var entry in ctx.ChangeTracker.Entries())
                {
                    if (entry.Entity.GetType().GetProperty("CreatedBy") != null)
                    {
                        entry.Property("CreatedBy").CurrentValue = userId;
                    }

                    if (entry.Entity.GetType().GetProperty("CreatedDate") != null)
                    {
                        entry.Property("CreatedDate").CurrentValue = DateTime.UtcNow;
                    }
                }
                await ctx.SaveChangesAsync(userId);

                var saved = await GetOrderAsync(order.Id);

                return (true, "", saved);
            }

            catch (Exception ex)
            {
                var fullError = ex.ToString(); // includes stack trace
                return (false, fullError, null);
            }


        }


        public async Task<(bool, string)> RecordPaymentAsync(RecordPaymentVM  recordPaymentVM, string userId)
            {
                var order = await ctx.SaleOrders
                    .Include(o => o.Payments)
                    .FirstOrDefaultAsync(o => o.Id == recordPaymentVM.SaleOrderId);

                if (order is null) return (false, "Order not found.");
                if (recordPaymentVM.Amount > order.AmountDue + 0.01m) return (false, "Payment exceeds amount due.");

                var payment = new Payment
                {
                   
                    CustomerId = order.CustomerId,
                    Amount = recordPaymentVM.Amount,
                    PaymentDate = DateTime.UtcNow,
                    Reference = recordPaymentVM.Reference,
                    Notes = recordPaymentVM.Notes,
                   
                };

                ctx.Payments.Add(payment);

                // Reload payments to recalculate
                var totalPaid = order.Payments.Sum(p => p.Amount) + recordPaymentVM.Amount;
                //order.PaymentStatus = totalPaid >= order.TotalAmount - 0.01m
                //    ? PaymentStatus.Paid
                //    : totalPaid > 0 ? PaymentStatus.Partial : PaymentStatus.Unpaid;

                await ctx.SaveChangesAsync();
                return (true, string.Empty);
            }

            public async Task<SaleOrder?> GetOrderAsync(int id) =>
                await ctx.SaleOrders
                    .Include(o => o.Items).ThenInclude(i => i.Product)
                    .Include(o => o.Customer)
                    .Include(o => o.Payments)
                   
                    .FirstOrDefaultAsync(o => o.Id == id);

            public async Task<List<SaleOrder>> GetOrdersAsync(
                DateTime? from, DateTime? to, PaymentStatus? status, SaleType? type)
            {
                var q = ctx.SaleOrders
                    .Include(o => o.Customer)
                    .Include(o => o.Items)
                    .Include(o => o.Payments)
                    .AsQueryable();

                if (from.HasValue) q = q.Where(o => o.OrderDate >= from.Value);
                if (to.HasValue) q = q.Where(o => o.OrderDate <= to.Value.AddDays(1));
                //if (status.HasValue) q = q.Where(o => o.PaymentStatus == status);
                //if (type.HasValue) q = q.Where(o => o.SaleType == type);

                return await q.OrderByDescending(o => o.OrderDate).ToListAsync();
            }

            public async Task<string> GenerateOrderNumberAsync()
            {
                var year = DateTime.Today.Year;
                var count = await ctx.SaleOrders.CountAsync(o => o.OrderDate.Year == year);
                return $"INV-{year}-{(count + 1):D5}";
            }    
   
        }



    }

