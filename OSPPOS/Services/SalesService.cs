
using Microsoft.EntityFrameworkCore;
using OSPPOS.Data;
using OSPPOS.Interfaces;
using OSPPOS.Models;
using OSPPOS.ViewModels;
using System;

namespace OSPPOS.Services
{


  

    public class SalesService(XContext ctx) : ISalesService
        {


        public async Task<(bool, string, SaleOrder?)> AddSaleAsync(AddSaleVM vm, string userId)
        {
            using var tx = await ctx.Database.BeginTransactionAsync();

            try
            {
                var productIds = vm.Items.Select(i => i.ProductId).ToList();

                var products = await ctx.Products
                    .Where(p => productIds.Contains(p.ProductId))
                    .ToDictionaryAsync(p => p.ProductId);

                // 🔒 STOCK VALIDATION
                foreach (var item in vm.Items)
                {
                    var product = products[item.ProductId];

                    if (product.CurrentStock < item.Quantity)
                        return (false, $"Insufficient stock for {product.Name}", null);
                }

                var order = new SaleOrder
                {
                    OrderNumber = await GenerateOrderNumberAsync(),
                    CustomerId = vm.CustomerId,
                    WalkInCustomerName = vm.WalkInCustomerName,
                    Notes = vm.Notes,
                    Discount = vm.Discount,
                    DiscountPercent = vm.DiscountPercent,
                    DueDate = vm.DueDate
                };

                foreach (var item in vm.Items)
                {
                    var product = products[item.ProductId];

                    order.Items.Add(new SaleOrderItem
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    });

                    // 🔥 STOCK UPDATE
                    product.CurrentStock -= item.Quantity;

                    ctx.StockMovements.Add(new StockMovement
                    {
                        ProductId = item.ProductId,
                        Quantity = -item.Quantity,
                        ReferenceId = order.Id,
                        Type = "SALE"
                    });
                }

                ctx.SaleOrders.Add(order);
                await ctx.SaveChangesAsync();

                // 🔥 INITIAL PAYMENT (optional, flexible)
                if (vm.InitialPayment > 0)
                {
                    ctx.Payments.Add(new Payment
                    {
                        SaleOrderId = order.Id,
                        Amount = vm.InitialPayment,
                        Method = vm.PaymentMethod,
                        Reference = vm.PaymentReference
                    });

                    await ctx.SaveChangesAsync();
                }

                await tx.CommitAsync();

                var saved = await GetOrderAsync(order.Id);
                return (true, "", saved);
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                return (false, ex.Message, null);
            }
        }

        public async Task<(bool, string)> RecordPaymentAsync(RecordPaymentVm vm, string userId)
            {
                var order = await ctx.SaleOrders
                    .Include(o => o.Payments)
                    .FirstOrDefaultAsync(o => o.Id == vm.SaleOrderId);

                if (order is null) return (false, "Order not found.");
                if (vm.Amount > order.AmountDue + 0.01m) return (false, "Payment exceeds amount due.");

                var payment = new Payment
                {
                    SaleOrderId = vm.SaleOrderId,
                    CustomerId = order.CustomerId,
                    Amount = vm.Amount,
                    
                    PaymentDate = DateTime.UtcNow,
                    Reference = vm.Reference,
                    Notes = vm.Notes,
                   
                };

                ctx.Payments.Add(payment);

                // Reload payments to recalculate
                var totalPaid = order.Payments.Sum(p => p.Amount) + vm.Amount;
                order.PaymentStatus = totalPaid >= order.TotalAmount - 0.01m
                    ? PaymentStatus.Paid
                    : totalPaid > 0 ? PaymentStatus.Partial : PaymentStatus.Unpaid;

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
                if (status.HasValue) q = q.Where(o => o.PaymentStatus == status);
                if (type.HasValue) q = q.Where(o => o.SaleType == type);

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

