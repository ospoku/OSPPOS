using OSPPOS.Models;
using OSPPOS.ViewModels;

namespace OSPPOS.Interfaces
{

        public interface ISalesService
        {
            Task<(bool Success, string Error, SaleOrder? Order)> AddSaleAsync(AddSaleVM vm, string userId);
            Task<(bool Success, string Error)> RecordPaymentAsync(RecordPaymentVm vm, string userId);
            Task<SaleOrder?> GetOrderAsync(int id);
            Task<List<SaleOrder>> GetOrdersAsync(DateTime? from, DateTime? to, PaymentStatus? status, SaleType? type);
            Task<string> GenerateOrderNumberAsync();
        }
    }

