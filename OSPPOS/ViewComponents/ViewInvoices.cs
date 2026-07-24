using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Data;
using OSPPOS.ViewModels;

namespace OSPPOS.ViewComponents
{

    public class ViewInvoices(XContext ctx) : Microsoft.AspNetCore.Mvc.ViewComponent
    {
   
        public IViewComponentResult Invoke()
        {
            var invoiceList = ctx.Invoices
                .ToList();

            var result = invoiceList.Select(i => new ViewInvoicesVM
            {
      Customer=i.Customer,
      Discount=i.Discount,
      CustomerId=i.CustomerId,
      WalkInCustomerName=i.WalkInCustomerName,
      DueDate=i.DueDate,
      InvoiceDate=i.InvoiceDate,
      InvoiceNumber=i.InvoiceNumber,
      Items=i.Items,
      Payments=i.Payments
            }).ToList();

            return View(result);
        }
    }
}