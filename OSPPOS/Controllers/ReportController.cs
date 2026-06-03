using Microsoft.AspNetCore.Mvc;

namespace ODS.Controllers
{
    public class ReportController:Controller
    {
        public ReportController()
        {

        }
        [HttpGet]
        public IActionResult ViewDashboard()
        {
            return ViewComponent(nameof(ViewDashboard));
        }
    }
}
