using Microsoft.AspNetCore.Mvc;
using RecipesWebApp.Server.Models;
using System.Diagnostics;

namespace RecipesWebApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // GET: Accommodation
        [HttpGet, ActionName("Index")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        // Post: Accommodation/Privacy
        [HttpGet]
        [Route("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        // Post: Accommodation/Error
        [HttpGet]
        [Route("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
