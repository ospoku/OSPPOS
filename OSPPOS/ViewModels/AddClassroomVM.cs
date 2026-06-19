using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Models;
using System.ComponentModel.DataAnnotations;




public class StockBatchItemVm
{
    [Required] public int ProductId { get; set; }
    [Required, Range(1, 999999)] public int Quantity { get; set; }
    [Required, Range(0.01, 999999)] public decimal UnitCost { get; set; }
    public DateTime? ExpiryDate { get; set; }
}




public class SaleItemVm
{
    [Required] public int ProductId { get; set; }
    [Required, Range(1, 999999)] public int Quantity { get; set; }
    [Required, Range(0.01, 999999)] public decimal UnitPrice { get; set; }
    public string? ProductName { get; set; }
}

// ─── Payment ───────────────────────────────────────────────────
public class RecordPaymentVm
{
    [Required] public int SaleOrderId { get; set; }
    [Required, Range(0.01, 9999999)] public decimal Amount { get; set; }
    public PaymentMethod Method { get; set; } 
    public string? Reference { get; set; }
    public string? Notes { get; set; }
}

// ─── Reports ───────────────────────────────────────────────────
public class SalesReportVm
{
    public DateTime From { get; set; } = DateTime.Today.AddDays(-30);
    public DateTime To { get; set; } = DateTime.Today;
    public List<SaleOrder> Orders { get; set; } = [];
    public decimal TotalRevenue => Orders.Sum(o => o.TotalAmount);
    public decimal TotalReceived => Orders.Sum(o => o.AmountPaid);
    public decimal TotalOutstanding => Orders.Sum(o => o.AmountDue);
    public int TotalTransactions => Orders.Count;
}

public class StockReportVm
{
    public List<Product> LowStockProducts { get; set; } = [];
    public List<Product> AllProducts { get; set; } = [];
    public decimal TotalStockValue => AllProducts.Sum(p => p.CurrentStock * p.CostPrice);
    public decimal TotalRetailValue => AllProducts.Sum(p => p.CurrentStock * p.SellingPrice);
}

public class DebtorReportVm
{
    public List<DebtorAgingVm> Debtors { get; set; } = [];
    public decimal TotalOutstanding => Debtors.Sum(d => d.TotalOwed);
}

public class DebtorAgingVm
{
    public Customer Customer { get; set; } = null!;
    public decimal Current { get; set; }      // 0-30 days
    public decimal Days30 { get; set; }       // 31-60
    public decimal Days60 { get; set; }       // 61-90
    public decimal Days90Plus { get; set; }   // 90+
    public decimal TotalOwed => Current + Days30 + Days60 + Days90Plus;
}

public class TopProductVm
{
    public Product Product { get; set; } = null!;
    public int TotalQtySold { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal TotalProfit { get; set; }
}

public class DashboardVm
{
    public decimal TodaySales { get; set; }
    public decimal MonthSales { get; set; }
    public decimal TotalOutstanding { get; set; }
    public int LowStockCount { get; set; }
    public int TodayTransactions { get; set; }
    public List<Product> LowStockItems { get; set; } = [];
    public List<SaleOrder> RecentSales { get; set; } = [];
}


