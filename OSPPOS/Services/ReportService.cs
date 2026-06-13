
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Data;
using OSPPOS.Interfaces;
using OSPPOS.Models;
using System;
using System.Drawing;

namespace OSPPOS.Services
{
  
       


    public class ReportService(XContext db) : IReportService
    {
        private readonly XContext ctx = db;

        public async Task<SalesReportVm> GetSalesReportAsync(DateTime from, DateTime to)
        {
            var orders = await ctx.SaleOrders
                .Include(o => o.Customer)
                .Include(o => o.Items)
                .Include(o => o.Payments)
                .Where(o => o.OrderDate >= from && o.OrderDate <= to.AddDays(1))
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return new SalesReportVm { From = from, To = to, Orders = orders };
        }

        public async Task<StockReportVm> GetStockReportAsync()
        {
            var products = await ctx.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Where(p => p.IsActive)
                .OrderBy(p => p.Category.Name).ThenBy(p => p.Name)
                .ToListAsync();

            return new StockReportVm
            {
                AllProducts = products,
                LowStockProducts = products.Where(p => p.IsLowStock).ToList()
            };
        }

        public async Task<DebtorReportVm> GetDebtorReportAsync()
        {
            var customers = await ctx.Customers
                .Include(c => c.SaleOrders).ThenInclude(o => o.Payments)
                .Where(c => c.SaleOrders.Any(o => o.PaymentStatus != PaymentStatus.Paid))
                .ToListAsync();

            var today = DateTime.Today;
            var debtors = customers.Select(c =>
            {
                var unpaidOrders = c.SaleOrders.Where(o => o.PaymentStatus != PaymentStatus.Paid);
                var aging = new DebtorAgingVm { Customer = c };
                foreach (var order in unpaidOrders)
                {
                    var dueDate = order.DueDate ?? order.OrderDate;
                    var days = (today - dueDate).Days;
                    var due = order.AmountDue;
                    if (days <= 30) aging.Current += due;
                    else if (days <= 60) aging.Days30 += due;
                    else if (days <= 90) aging.Days60 += due;
                    else aging.Days90Plus += due;
                }
                return aging;
            }).Where(a => a.TotalOwed > 0).ToList();

            return new DebtorReportVm { Debtors = debtors };
        }

        public async Task<List<TopProductVm>> GetTopProductsAsync(DateTime from, DateTime to, int top = 10)
        {
            var items = await ctx.SaleOrderItems
                .Include(i => i.Product).ThenInclude(p => p.Category)
                .Include(i => i.SaleOrder)
                .Where(i => i.SaleOrder.OrderDate >= from && i.SaleOrder.OrderDate <= to.AddDays(1))
                .ToListAsync();

            return items
                .GroupBy(i => i.ProductId)
                .Select(g => new TopProductVm
                {
                    Product = g.First().Product,
                    TotalQtySold = g.Sum(i => i.Quantity),
                    TotalRevenue = g.Sum(i => i.LineTotal),
                    TotalProfit = g.Sum(i => i.LineTotal - i.Quantity * i.Product.CostPrice)
                })
                .OrderByDescending(v => v.TotalRevenue)
                .Take(top)
                .ToList();
        }

        public async Task<DashboardVm> GetDashboardAsync()
        {
            var today = DateTime.Today;
            var monthStart = new DateTime(today.Year, today.Month, 1);

            var allOrders = await ctx.SaleOrders
                .Include(o => o.Customer)
                .Include(o => o.Items)
                .Include(o => o.Payments)
                .ToListAsync();

            var todayOrders = allOrders.Where(o => o.OrderDate.Date == today).ToList();
            var monthOrders = allOrders.Where(o => o.OrderDate >= monthStart).ToList();
            var recentOrders = allOrders.OrderByDescending(o => o.OrderDate).Take(10).ToList();
            var lowStockItems = await ctx.Products
                .Include(p => p.Category)
                .Where(p => p.IsActive && p.CurrentStock <= p.ReorderLevel)
                .Take(10).ToListAsync();

            return new DashboardVm
            {
                TodaySales = todayOrders.Sum(o => o.TotalAmount),
                TodayTransactions = todayOrders.Count,
                MonthSales = monthOrders.Sum(o => o.TotalAmount),
                TotalOutstanding = allOrders.Sum(o => o.AmountDue),
                LowStockCount = lowStockItems.Count,
                LowStockItems = lowStockItems,
                RecentSales = recentOrders
            };
        }

        // ─── Excel Exports ─────────────────────────────────────────
        public async Task<byte[]> ExportSalesToExcelAsync(DateTime from, DateTime to)
        {
            var report = await GetSalesReportAsync(from, to);
            using var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Sales");

            ws.Cell(1, 1).Value = "Sales Report";
            ws.Cell(1, 1).Style.Font.Bold = true;
            ws.Cell(1, 1).Style.Font.FontSize = 14;
            ws.Cell(2, 1).Value = $"Period: {from:dd/MM/yyyy} – {to:dd/MM/yyyy}";

            string[] headers = ["Invoice #", "Date", "Customer", "Type", "Amount", "Paid", "Balance", "Status"];
            for (int i = 0; i < headers.Length; i++)
            {
                ws.Cell(4, i + 1).Value = headers[i];
                ws.Cell(4, i + 1).Style.Font.Bold = true;
                ws.Cell(4, i + 1).Style.Fill.BackgroundColor = XLColor.FromHtml("#1a3c5e");
                ws.Cell(4, i + 1).Style.Font.FontColor = XLColor.White;
            }

            int row = 5;
            foreach (var o in report.Orders)
            {
                ws.Cell(row, 1).Value = o.OrderNumber;
                ws.Cell(row, 2).Value = o.OrderDate.ToString("dd/MM/yyyy HH:mm");
                ws.Cell(row, 3).Value = o.CustomerDisplay;
                ws.Cell(row, 4).Value = o.SaleType.ToString();
                ws.Cell(row, 5).Value = (double)o.TotalAmount;
                ws.Cell(row, 6).Value = (double)o.AmountPaid;
                ws.Cell(row, 7).Value = (double)o.AmountDue;
                ws.Cell(row, 8).Value = o.PaymentStatus.ToString();
                ws.Cell(row, 5).Style.NumberFormat.Format = "#,##0.00";
                ws.Cell(row, 6).Style.NumberFormat.Format = "#,##0.00";
                ws.Cell(row, 7).Style.NumberFormat.Format = "#,##0.00";
                row++;
            }

            // Totals
            ws.Cell(row, 4).Value = "TOTAL";
            ws.Cell(row, 5).Value = (double)report.TotalRevenue;
            ws.Cell(row, 6).Value = (double)report.TotalReceived;
            ws.Cell(row, 7).Value = (double)report.TotalOutstanding;
            for (int c = 4; c <= 7; c++)
            {
                ws.Cell(row, c).Style.Font.Bold = true;
                ws.Cell(row, c).Style.NumberFormat.Format = "#,##0.00";
            }

            ws.Columns().AdjustToContents();
            using var ms = new MemoryStream();
            wb.SaveAs(ms);
            return ms.ToArray();
        }

        public async Task<byte[]> ExportStockToExcelAsync()
        {
            var report = await GetStockReportAsync();
            using var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Stock Levels");

            ws.Cell(1, 1).Value = "Stock Report";
            ws.Cell(1, 1).Style.Font.Bold = true;
            ws.Cell(1, 1).Style.Font.FontSize = 14;
            ws.Cell(2, 1).Value = $"Generated: {DateTime.Now:dd/MM/yyyy HH:mm}";

            string[] headers = ["SKU", "Product", "Category", "Unit", "In Stock", "Reorder Level", "Cost Price", "Selling Price", "Stock Value", "Status"];
            for (int i = 0; i < headers.Length; i++)
            {
                ws.Cell(4, i + 1).Value = headers[i];
                ws.Cell(4, i + 1).Style.Font.Bold = true;
                ws.Cell(4, i + 1).Style.Fill.BackgroundColor = XLColor.FromHtml("#1a3c5e");
                ws.Cell(4, i + 1).Style.Font.FontColor = XLColor.White;
            }

            int row = 5;
            foreach (var p in report.AllProducts)
            {
                ws.Cell(row, 1).Value = p.SKU;
                ws.Cell(row, 2).Value = p.Name;
                ws.Cell(row, 3).Value = p.Category?.Name;
                ws.Cell(row, 4).Value = p.UnitId;
                ws.Cell(row, 5).Value = p.CurrentStock;
                ws.Cell(row, 6).Value = p.ReorderLevel;
                ws.Cell(row, 7).Value = (double)p.CostPrice;
                ws.Cell(row, 8).Value = (double)p.SellingPrice;
                ws.Cell(row, 9).Value = (double)(p.CurrentStock * p.CostPrice);
                ws.Cell(row, 10).Value = p.IsLowStock ? "LOW STOCK" : "OK";
                ws.Cell(row, 7).Style.NumberFormat.Format = "#,##0.00";
                ws.Cell(row, 8).Style.NumberFormat.Format = "#,##0.00";
                ws.Cell(row, 9).Style.NumberFormat.Format = "#,##0.00";
                if (p.IsLowStock)
                    ws.Row(row).Style.Fill.BackgroundColor = XLColor.FromHtml("#fff3cd");
                row++;
            }

            ws.Columns().AdjustToContents();
            using var ms = new MemoryStream();
            wb.SaveAs(ms);
            return ms.ToArray();
        }

        public async Task<byte[]> ExportDebtorsToExcelAsync()
        {
            var report = await GetDebtorReportAsync();
            using var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Debtors");

            ws.Cell(1, 1).Value = "Debtor Aging Report";
            ws.Cell(1, 1).Style.Font.Bold = true;
            ws.Cell(1, 1).Style.Font.FontSize = 14;

            string[] headers = ["Customer", "Phone", "Current (0-30)", "31-60 days", "61-90 days", "90+ days", "Total Owed"];
            for (int i = 0; i < headers.Length; i++)
            {
                ws.Cell(3, i + 1).Value = headers[i];
                ws.Cell(3, i + 1).Style.Font.Bold = true;
                ws.Cell(3, i + 1).Style.Fill.BackgroundColor = XLColor.FromHtml("#1a3c5e");
                ws.Cell(3, i + 1).Style.Font.FontColor = XLColor.White;
            }

            int row = 4;
            foreach (var d in report.Debtors)
            {
                ws.Cell(row, 1).Value = d.Customer.Name;
                ws.Cell(row, 2).Value = d.Customer.Phone;
                ws.Cell(row, 3).Value = (double)d.Current;
                ws.Cell(row, 4).Value = (double)d.Days30;
                ws.Cell(row, 5).Value = (double)d.Days60;
                ws.Cell(row, 6).Value = (double)d.Days90Plus;
                ws.Cell(row, 7).Value = (double)d.TotalOwed;
                for (int c = 3; c <= 7; c++)
                    ws.Cell(row, c).Style.NumberFormat.Format = "#,##0.00";
                row++;
            }

            ws.Columns().AdjustToContents();
            using var ms = new MemoryStream();
            wb.SaveAs(ms);
            return ms.ToArray();
        }
    } }



