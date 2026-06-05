using OSPPOS.Models;
using OSPPOS.ViewModels;

namespace OSPPOS.Interfaces
{
    public interface IStockService
    {    
        Task<StockBatch> CreateGRNAsync(AddStockBatchVM vm, string userId);
        Task<List<StockBatch>> GetAllGRNsAsync(DateTime? from = null, DateTime? to = null);
        Task<StockBatch?> GetGRNAsync(int id);
        Task<string> GenerateGRNNumberAsync();
    }
}
