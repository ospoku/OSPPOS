using OSPPOS.ViewModels;

namespace OSPPOS.Interfaces
{
   
        public interface IReportService
        {
            Task<SalesReportVM> GetSalesReportAsync(DateTime from, DateTime to);
            Task<StockReportVM> GetStockReportAsync();
            Task<DebtorReportVM> GetDebtorReportAsync();
            Task<List<TopProductVM>> GetTopProductsAsync(DateTime from, DateTime to, int top = 10);
            Task<DashboardVM> GetDashboardAsync();
            Task<byte[]> ExportSalesToExcelAsync(DateTime from, DateTime to);
            Task<byte[]> ExportStockToExcelAsync();
            Task<byte[]> ExportDebtorsToExcelAsync();
        }
    }

