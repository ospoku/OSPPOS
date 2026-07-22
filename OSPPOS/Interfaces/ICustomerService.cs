using OSPPOS.Models;
using OSPPOS.ViewModels;
using System.Security.Claims;

namespace OSPPOS.Interfaces
{
    public interface ICustomerService
    {
  
        
            Task<bool> AddCustomerAsync(AddCustomerVM vm, ClaimsPrincipal user);

            Task<Customer?> GetCustomerForEdit(Guid publicId);

            Task<bool> UpdateCustomerAsync(Customer customer);

            Task<Customer?> GetCustomerStatementAsync(int id);
        
    }
}

