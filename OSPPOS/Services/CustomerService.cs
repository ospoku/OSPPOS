using Microsoft.EntityFrameworkCore;
using OSPPOS.Data;
using OSPPOS.DTO.Customer;
using OSPPOS.Interfaces;
using OSPPOS.Models;
using OSPPOS.ViewModels;
using System.Security.Claims;

namespace OSPPOS.Services
{
    public class CustomerService(XContext ctx, EntityService entityService):ICustomerService
    {
   
        private readonly EntityService _entityService = entityService;

        public async Task<bool> AddCustomerAsync(AddCustomerVM vm, ClaimsPrincipal user)
        {
            var customer = new Customer
            {
                Name = vm.Name,
                Email = vm.Email,
                Phone = vm.Phone,
                Address = vm.Address,
                TaxNumber = vm.TaxNumber,
                CreditLimit = vm.AllowCredit ? vm.CreditLimit : 0,
                AllowCredit = vm.AllowCredit,
                IsActive = vm.IsActive
            };

            return await _entityService.AddEntityAsync(customer, user);
        }

        public async Task<Customer?> GetCustomerForEdit(Guid publicId)
        {
            return await ctx.Customers
                .FirstOrDefaultAsync(c => c.PublicId == publicId);
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            var existing = await ctx.Customers
                .FirstOrDefaultAsync(c => c.PublicId == customer.PublicId);

            if (existing == null)
                return false;

            existing.Address = customer.Address;
            existing.TaxNumber = customer.TaxNumber;

            ctx.Customers.Update(existing);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<Customer?> GetCustomerStatementAsync(int id)
        {
            return await ctx.Customers
                .Include(x => x.SaleOrders)
                    .ThenInclude(o => o.Items)
                        .ThenInclude(i => i.Product)
                .Include(x => x.SaleOrders)
                .FirstOrDefaultAsync(x => x.CustomerId == id);
        }
        public async Task<List<ViewCustomersDTO>> ViewCustomersAsync(ViewCustomersDTO viewCustomersDTO)
        {
            var customers = ctx.Customers.Select(c => new ViewCustomersDTO() {Name=c.Name }).ToListAsync();
           
            
            return await customers;
        }

        Task<bool> ICustomerService.AddCustomerAsync(AddCustomerVM vm, ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }

        Task<Customer?> ICustomerService.GetCustomerForEdit(Guid publicId)
        {
            throw new NotImplementedException();
        }

   

        Task<bool> ICustomerService.UpdateCustomerAsync(Customer customer)
        {
            throw new NotImplementedException();
        }

        Task<Customer?> ICustomerService.GetCustomerStatementAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}