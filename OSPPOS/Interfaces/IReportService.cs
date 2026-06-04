namespace OSPPOS.Interfaces
{
   
        public interface IReportService
        {
            Task<SalesReportVm> GetSalesReportAsync(DateTime from, DateTime to);
            Task<StockReportVm> GetStockReportAsync();
            Task<DebtorReportVm> GetDebtorReportAsync();
            Task<List<TopProductVm>> GetTopProductsAsync(DateTime from, DateTime to, int top = 10);
            Task<DashboardVm> GetDashboardAsync();
            Task<byte[]> ExportSalesToExcelAsync(DateTime from, DateTime to);
            Task<byte[]> ExportStockToExcelAsync();
            Task<byte[]> ExportDebtorsToExcelAsync();
        }
    }

