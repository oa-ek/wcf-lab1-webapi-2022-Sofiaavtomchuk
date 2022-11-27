using Microsoft.AspNetCore.Mvc;
using Recipes.Core;
using Recipes.Repos;

namespace RecipesWebApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _categoryRepository;
        public CategoryController(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: Accommodation
        [HttpGet, ActionName("Index")]
        [Route("Index")]
        public IActionResult Index()
        {
            var categories = _categoryRepository.GetCategories();
            return View(categories);
        }
        // GET: Accommodation/Create
        [NonAction]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                var createdCategories = await _categoryRepository.AddCategoryAsync(category);
                return RedirectToAction("Edit", "Category", new { id = createdCategories.Id });
            }
            return View(category);
        }
        // Post: Accommodation/Edit
        [NonAction]
        public IActionResult Edit(int id)
        {
            return View(_categoryRepository.GetCategory(id));
        }
        // POST: Accommodation/Edit
        [HttpGet]
        [Route("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryRepository.UpdateCategoryAsync(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }
        // GET: Accommodation/Delete
        [HttpGet]
        [Route("Delete")]
        public IActionResult Delete(int id)
        {
            return View(_categoryRepository.GetCategory(id));
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Category category)
        {
            await _categoryRepository.DeleteCategoryAsync(category.Id);
            return RedirectToAction("Index");
        }
    }
}
