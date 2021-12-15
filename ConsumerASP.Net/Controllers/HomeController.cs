using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ConsumerASP.Net.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return new JsonResult(new { Home = "Home" });
        }

        public IActionResult Privacy()
        {
            return new JsonResult(new { Home = "Home" });
        }
    }
}