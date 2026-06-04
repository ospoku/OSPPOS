using OSPPOS.Models;

namespace OSPPOS.Interfaces
{
    public interface IStockService
    {    
        Task<StockBatch> CreateGRNAsync(CreateStockBatchVm vm, string userId);
        Task<List<StockBatch>> GetAllGRNsAsync(DateTime? from = null, DateTime? to = null);
        Task<StockBatch?> GetGRNAsync(int id);
        Task<string> GenerateGRNNumberAsync();
    }
}
