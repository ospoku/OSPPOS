using Microsoft.EntityFrameworkCore;
using OSPPOS.Data;
using OSPPOS.Models;
using OSPPOS.ViewModels;
using System.Security.Claims;

namespace OSPPOS.Services
{
    public class CustomerService
    {
        private readonly XContext _ctx;
        private readonly EntityService _entityService;

        public CustomerService(XContext ctx, EntityService entityService)
        {
            _ctx = ctx;
            _entityService = entityService;
        }

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
            return await _ctx.Customers
                .FirstOrDefaultAsync(c => c.PublicId == publicId);
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            var existing = await _ctx.Customers
                .FirstOrDefaultAsync(c => c.PublicId == customer.PublicId);

            if (existing == null)
                return false;

            existing.Address = customer.Address;
            existing.TaxNumber = customer.TaxNumber;

            _ctx.Customers.Update(existing);
            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<Customer?> GetCustomerStatementAsync(int id)
        {
            return await _ctx.Customers
                .Include(x => x.SaleOrders)
                    .ThenInclude(o => o.Items)
                        .ThenInclude(i => i.Product)
                .Include(x => x.SaleOrders)
                .FirstOrDefaultAsync(x => x.CustomerId == id);
        }
    }
}