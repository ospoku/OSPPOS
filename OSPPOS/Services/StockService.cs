
using Microsoft.EntityFrameworkCore;
using OSPPOS.Data;
using OSPPOS.Interfaces;
using OSPPOS.Models;
using System;

namespace OSPPOS.Services
{
  

    public class StockService(XContext db) : IStockService
    {
        private readonly XContext ctx = db;

        public async Task<StockBatch> CreateGRNAsync(CreateStockBatchVm vm, string userId)
        {
            var grn = new StockBatch
            {
                GRNNumber = await GenerateGRNNumberAsync(),
                SupplierId = vm.SupplierId,
                ReceivedDate = vm.ReceivedDate,
                SupplierInvoiceRef = vm.SupplierInvoiceRef,
                Notes = vm.Notes,
                ReceivedById = userId
            };

            foreach (var item in vm.Items.Where(i => i.Quantity > 0))
            {
                grn.Items.Add(new StockBatchItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitCost = item.UnitCost,
                    ExpiryDate = item.ExpiryDate
                });

                // Update current stock
                var product = await ctx.Products.FindAsync(item.ProductId);
                if (product is not null)
                {
                    product.CurrentStock += item.Quantity;
                    product.CostPrice = item.UnitCost;   // update with latest cost
                }
            }

            ctx.StockBatches.Add(grn);
            await ctx.SaveChangesAsync();
            return grn;
        }

        public async Task<List<StockBatch>> GetAllGRNsAsync(DateTime? from = null, DateTime? to = null)
        {
            var query = ctx.StockBatches
                .Include(b => b.Supplier)
                .Include(b => b.Items).ThenInclude(i => i.Product)
                .Include(b => b.ReceivedBy)
                .AsQueryable();

            if (from.HasValue) query = query.Where(b => b.ReceivedDate >= from.Value);
            if (to.HasValue) query = query.Where(b => b.ReceivedDate <= to.Value.AddDays(1));

            return await query.OrderByDescending(b => b.ReceivedDate).ToListAsync();
        }

        public async Task<StockBatch?> GetGRNAsync(int id) =>
            await ctx.StockBatches
                .Include(b => b.Supplier)
                .Include(b => b.Items).ThenInclude(i => i.Product)
                .Include(b => b.ReceivedBy)
                .FirstOrDefaultAsync(b => b.Id == id);

        public async Task<string> GenerateGRNNumberAsync()
        {
            var year = DateTime.Today.Year;
            var count = await ctx.StockBatches.CountAsync(b => b.ReceivedDate.Year == year);
            return $"GRN-{year}-{(count + 1):D4}";
        }
    }


}

