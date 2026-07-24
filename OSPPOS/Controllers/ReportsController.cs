
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OSPPOS.Interfaces;

namespace OSPPOS.Controllers;

[Authorize(Roles = "Admin,Manager")]
public class ReportsController : Controller
{
    private readonly IReportService _reports;
    public ReportsController(IReportService reports) => _reports = reports;

    public IActionResult Index() => View();

    public async Task<IActionResult> Sales(DateTime? from, DateTime? to)
    {
        from ??= DateTime.Today.AddDays(-30);
        to ??= DateTime.Today;
        var vm = await _reports.GetSalesReportAsync(from.Value, to.Value);
        return View(vm);
    }

    public async Task<IActionResult> Stock() =>
        View(await _reports.GetStockReportAsync());

    public async Task<IActionResult> Debtors() =>
        View(await _reports.GetDebtorReportAsync());

    public async Task<IActionResult> TopProducts(DateTime? from, DateTime? to)
    {
        from ??= DateTime.Today.AddDays(-30);
        to ??= DateTime.Today;
        var vm = await _reports.GetTopProductsAsync(from.Value, to.Value);
        ViewBag.From = from.Value.ToString("yyyy-MM-dd");
        ViewBag.To = to.Value.ToString("yyyy-MM-dd");
        return View(vm);
    }

    // Excel downloads
    public async Task<IActionResult> DownloadSales(DateTime? from, DateTime? to)
    {
        from ??= DateTime.Today.AddDays(-30);
        to ??= DateTime.Today;
        var bytes = await _reports.ExportSalesToExcelAsync(from.Value, to.Value);
        return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            $"Sales_{from:yyyyMMdd}_{to:yyyyMMdd}.xlsx");
    }

    public async Task<IActionResult> DownloadStock()
    {
        var bytes = await _reports.ExportStockToExcelAsync();
        return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            $"Stock_{DateTime.Today:yyyyMMdd}.xlsx");
    }

    public async Task<IActionResult> DownloadDebtors()
    {
        var bytes = await _reports.ExportDebtorsToExcelAsync();
        return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            $"Debtors_{DateTime.Today:yyyyMMdd}.xlsx");
    }
}
