
using Microsoft.EntityFrameworkCore;
using OSPPOS.Models;
using System;

namespace OSPPOS.Services
{
   

        public interface ISalesService
        {
            Task<(bool Success, string Error, SaleOrder? Order)> CreateSaleAsync(CreateSaleVm vm, string userId);
            Task<(bool Success, string Error)> RecordPaymentAsync(RecordPaymentVm vm, string userId);
            Task<SaleOrder?> GetOrderAsync(int id);
            Task<List<SaleOrder>> GetOrdersAsync(DateTime? from, DateTime? to, PaymentStatus? status, SaleType? type);
            Task<string> GenerateOrderNumberAsync();
        }

        public class SalesService : ISalesService
        {
            private readonly XContext ctx;
        public SalesService(XContext ctx) => this.ctx = ctx;

            public async Task<(bool, string, SaleOrder?)> CreateSaleAsync(CreateSaleVm vm, string userId)
            {
                // Validate stock availability
                foreach (var item in vm.Items)
                {
                    var product = await ctx.Products.FindAsync(item.ProductId);
                    if (product is null) return (false, $"Product id {item.ProductId} not found.", null);
                    if (product.CurrentStock < item.Quantity)
                        return (false, $"Insufficient stock for {product.Name}. Available: {product.CurrentStock}", null);
                }

                var order = new SaleOrder
                {
                    OrderNumber = await GenerateOrderNumberAsync(),
                    CustomerId = vm.CustomerId,
                    WalkInCustomerName = vm.WalkInCustomerName,
                    SaleType = vm.SaleType,
                    OrderDate = DateTime.UtcNow,
                    DueDate = vm.DueDate,
                    Notes = vm.Notes,
                    Discount = vm.Discount,
                    DiscountPercent = vm.DiscountPercent,
                    PaymentStatus = PaymentStatus.Unpaid,
                    SoldById = userId
                };

                foreach (var item in vm.Items)
                {
                    order.Items.Add(new SaleOrderItem
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    });

                    // Deduct stock
                    var product = await ctx.Products.FindAsync(item.ProductId);
                    if (product is not null) product.CurrentStock -= item.Quantity;
                }

                ctx.SaleOrders.Add(order);
                await ctx   .SaveChangesAsync();

                // Record initial payment for cash sales
                if (vm.SaleType == SaleType.Cash && vm.CashReceived > 0)
                {
                    var payVm = new RecordPaymentVm
                    {
                        SaleOrderId = order.Id,
                        Amount = Math.Min(vm.CashReceived, order.TotalAmount),
                        Method = vm.PaymentMethod,
                        Reference = vm.PaymentReference
                    };
                    await RecordPaymentAsync(payVm, userId);
                }

                // Reload with navigation props
                var saved = await GetOrderAsync(order.Id);
                return (true, string.Empty, saved);
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
                    Method = vm.Method,
                    PaymentDate = DateTime.UtcNow,
                    Reference = vm.Reference,
                    Notes = vm.Notes,
                    RecordedById = userId
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
                    .Include(o => o.SoldBy)
                    .FirstOrDefaultAsync(o => o.Id == id);

            public async Task<List<SaleOrder>> GetOrdersAsync(
                DateTime? from, DateTime? to, PaymentStatus? status, SaleType? type)
            {
                var q = _db.SaleOrders
                    .Include(o => o.Customer)
                    .Include(o => o.Items)
                    .Include(o => o.Payments)
                    .AsQueryable();

                if (from.HasValue) q = q.Where(o => o.OrderDate >= from.Value);
                if (to.HasValue) q = q.Where(o => o.OrderDate <= to.Value.AddDays(1));
                if (status.HasValue) q = q.Where(o => o.PaymentStatus == status.Value);
                if (type.HasValue) q = q.Where(o => o.SaleType == type.Value);

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
}
