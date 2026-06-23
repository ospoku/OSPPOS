using OSPPOS.ViewModels;

namespace OSPPOS.Interfaces
{
   
        public interface IReportService
        {
            Task<SalesReportVM> GetSalesReportAsync(DateTime from, DateTime to);
            Task<StockReportVM> GetStockReportAsync();
            Task<DebtorReportVm> GetDebtorReportAsync();
            Task<List<TopProductVm>> GetTopProductsAsync(DateTime from, DateTime to, int top = 10);
            Task<DashboardVM> GetDashboardAsync();
            Task<byte[]> ExportSalesToExcelAsync(DateTime from, DateTime to);
            Task<byte[]> ExportStockToExcelAsync();
            Task<byte[]> ExportDebtorsToExcelAsync();
        }
    }

