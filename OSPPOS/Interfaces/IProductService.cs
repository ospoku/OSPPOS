using OSPPOS.DTO;
using OSPPOS.Models;
using OSPPOS.ViewModels;
using System.Security.Claims;

namespace OSPPOS.Interfaces
{
    public interface IProductService
    {

        Task<(bool Success, string Error, Product? Product)> AddProductAsync(AddProductDTO addProductDTO, ClaimsPrincipal user);
        Task<(bool Success, string Error)> RecordPaymentAsync(RecordPaymentVM vm, string userId);
        Task<SaleOrder?> GetOrderAsync(int id);
        Task<List<SaleOrder>> GetOrdersAsync(DateTime? from, DateTime? to, PaymentStatus? status, SaleType? type);
        Task<string> GenerateOrderNumberAsync();
    }
}
