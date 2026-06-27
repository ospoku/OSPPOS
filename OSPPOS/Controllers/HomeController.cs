using Microsoft.AspNetCore.Mvc;


namespace OSPPOS.Controllers
{
    public class HomeController(ILogger<HomeController> logger) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        

        public IActionResult Index()
        {
            
  

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }

        public IActionResult About()
        {
         
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
      
    }
}
