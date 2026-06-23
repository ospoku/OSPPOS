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
public class RecordPaymentVM
{
    [Required] public int SaleOrderId { get; set; }
    [Required, Range(0.01, 9999999)] public decimal Amount { get; set; }
    public PaymentMethod Method { get; set; } 
    public string? Reference { get; set; }
    public string? Notes { get; set; }
}

// ─── Reports ───────────────────────────────────────────────────



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




