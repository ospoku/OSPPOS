using OSPPOS.DTO.Customer;
using OSPPOS.DTO.Invoice;
using OSPPOS.Models;
using OSPPOS.ViewModels;
using System.Security.Claims;

namespace OSPPOS.Interfaces
{
    public interface IInvoiceService
    {
        Task<(bool Success, string Error, Invoice? Invoice)> AddInvoiceAsync(AddInvoiceDTO addInvoiceDTO, ClaimsPrincipal user);
        Task<List<ViewInvoicesDTO>> ViewInvoicesAsync(ViewInvoicesDTO viewInvoicesDTO);
        Task<(bool Success, string Error)> RecordPaymentAsync(RecordPaymentVM vm, string userId);
        Task<Invoice?> GetInvoiceAsync(int id);
        Task<List<Invoice>> GetInvoicesAsync(DateTime? from, DateTime? to, PaymentStatus? status, SaleType? type);
        Task<bool> DeleteInvoiceAsync(int id);
        Task<bool> UpdateInvoiceAsync(UpdateInvoiceDTO dto, ClaimsPrincipal user);
        Task<string?> GetInvoiceForEditAsync(int id);
      
    }
}
