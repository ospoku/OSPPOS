using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using OSPPOS.Data;
using OSPPOS.Models;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{
    public class ViewStocks(XContext ctx) : ViewComponent
    {
       

        public IViewComponentResult Invoke()
        {
            

          var stocks = ctx.StockBatches.Select(s => new ViewStocksVM 
            {
                SupplierId=s.Supplier.Name,
                SupplierInvoice=s.SupplierInvoiceRef,
                Notes=s.Notes,
                StockBatchItems=s.Items.ToList() }
            ).ToList();
    

            return View(stocks);
        }
    }
}
