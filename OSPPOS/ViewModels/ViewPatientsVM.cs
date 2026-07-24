using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Models;
using System;
using System.ComponentModel.DataAnnotations;
using static DMX.Constants.Permissions;




namespace OSPPOS.ViewModels
{
    public class ViewPatientVM
    {
     
      
        public string Name { get; set; }
    
        public bool IsActive { get; set; }
        public bool IsLowStock { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanInsert { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanPrint { get; set; }
        public bool CanSave { get; set; }
        public bool CanView { get; set; }
        public Guid PublicId { get; set; }
      
       
        public string Unit { get; set; }
        public decimal CostPrice { get; set; }

        public decimal SellingPrice { get; set; }
        public decimal WholesalePrice { get; set; }
        public decimal ProfitMargin { get; set; }
        public int ReorderLevel { get; set; }
        public int CurrentStock { get; set; }
     
        public int TotalProducts { get; set; }

        public decimal TotalStockCost { get; set; }
        public decimal TotalRetailValue { get; set; }
       
       
    }
}
                      
    


