using OSPPOS.Data;
using OSPPOS.Models;

namespace OSPPOS.Services
{
    public class StockLedgerService(XContext context)
    {
        private readonly XContext dcx = context;

        public async Task AddStockLedgerEntry(
    int productId,
    int qtyIn,
    int qtyOut,
    string reference,
    string type)
        {
            var lastBalance = dcx.StockLedgers
                .Where(x => x.ProductId == productId)
                .OrderByDescending(x => x.Id)
                .Select(x => x.Balance)
                .FirstOrDefault();

            var newBalance = lastBalance + qtyIn - qtyOut;

            var entry = new StockLedger
            {
                ProductId = productId,
                Date = DateTime.UtcNow,
                QuantityIn = qtyIn,
                QuantityOut = qtyOut,
                Balance = newBalance,
                Reference = reference,
                TransactionType = type
            };

            dcx.StockLedgers.Add(entry);
            await dcx.SaveChangesAsync();
        }
    }
}
