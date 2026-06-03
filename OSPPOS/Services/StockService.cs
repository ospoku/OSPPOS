using DrinksPOS.ViewModels;
using OSPPOS.Models;
using System;

namespace OSPPOS.Services
{
    public class StockService
    {

       
    public interface IStockService
    {
        Task<StockBatch> CreateGRNAsync(CreateStockBatchVm vm, string userId);
        Task<List<StockBatch>> GetAllGRNsAsync(DateTime? from = null, DateTime? to = null);
        Task<StockBatch?> GetGRNAsync(int id);
        Task<string> GenerateGRNNumberAsync();
    }

    public class StockService : IStockService
    {
        private readonly AppDbContext _db;
        public StockService(AppDbContext db) => _db = db;

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
                var product = await _db.Products.FindAsync(item.ProductId);
                if (product is not null)
                {
                    product.CurrentStock += item.Quantity;
                    product.CostPrice = item.UnitCost;   // update with latest cost
                }
            }

            _db.StockBatches.Add(grn);
            await _db.SaveChangesAsync();
            return grn;
        }

        public async Task<List<StockBatch>> GetAllGRNsAsync(DateTime? from = null, DateTime? to = null)
        {
            var query = _db.StockBatches
                .Include(b => b.Supplier)
                .Include(b => b.Items).ThenInclude(i => i.Product)
                .Include(b => b.ReceivedBy)
                .AsQueryable();

            if (from.HasValue) query = query.Where(b => b.ReceivedDate >= from.Value);
            if (to.HasValue) query = query.Where(b => b.ReceivedDate <= to.Value.AddDays(1));

            return await query.OrderByDescending(b => b.ReceivedDate).ToListAsync();
        }

        public async Task<StockBatch?> GetGRNAsync(int id) =>
            await _db.StockBatches
                .Include(b => b.Supplier)
                .Include(b => b.Items).ThenInclude(i => i.Product)
                .Include(b => b.ReceivedBy)
                .FirstOrDefaultAsync(b => b.Id == id);

        public async Task<string> GenerateGRNNumberAsync()
        {
            var year = DateTime.Today.Year;
            var count = await _db.StockBatches.CountAsync(b => b.ReceivedDate.Year == year);
            return $"GRN-{year}-{(count + 1):D4}";
        }
    }


}
}
