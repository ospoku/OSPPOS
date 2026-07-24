using Microsoft.EntityFrameworkCore;
using OSPPOS.Data;
using OSPPOS.Interfaces;

namespace OSPPOS.Services
{
    public class FinancialService(XContext ctx):IFinancialService
    {

        public async Task<decimal> GetCustomerCredit(int CustomerId)
        {

            return await ctx.Invoices.Where(c => c.CustomerId == CustomerId).SumAsync(c => c.TotalAmount - c.Payments.Sum(p => p.Amount));
        }
    }
}
