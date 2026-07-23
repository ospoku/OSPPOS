using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;

namespace OSPPOS.ViewComponents
{
    public class Organization:ViewComponent
    {

        public IViewComponentResult InvokeAsync()
        {

            return View();
        }
      
    }
}
