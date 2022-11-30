using Microsoft.AspNetCore.Mvc;

namespace RecipesBlazorApp.Server.Controllers
{
    public class VCategorryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
