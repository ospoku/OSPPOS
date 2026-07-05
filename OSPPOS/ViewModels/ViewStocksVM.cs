using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using OSPPOS.Models;
using System;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OSPPOS.ViewModels
{
    public class ViewStocksVM
    {

                public string SupplierId { get; set; }
                public DateTime DateReceived { get; set; }

                   public  List<StockBatchItem> StockBatchItems { get; set; }
        public string SupplierInvoice { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
                   
                public string UnitCost { get; set; }
          public DateTime ExpiryDate { get; set; }
       
       
    
        public DateTime? DueDate { get; set; }
    
        public decimal DiscountPercent { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
        public List<SaleItemVM> Items { get; set; } = [];

        // For initial cash payment
      
       
      
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; } 
        public bool CanInsert { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanPrint { get; set; }
        public string PublicId { get; set; }
    }
}



