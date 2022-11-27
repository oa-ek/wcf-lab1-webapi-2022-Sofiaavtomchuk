using Recipes.Core;
using Recipes.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Drawing.Drawing2D;

namespace KeyShop.UI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _categoryRepository;
        public CategoryController(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            var categories = _categoryRepository.GetCategories();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                var createdCategories = await _categoryRepository.AddCategoryAsync(category);
                return RedirectToAction("Edit", "Category", new { id = createdCategories.Id });
            }
            return View(category);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(_categoryRepository.GetCategory(id));
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryRepository.UpdateCategoryAsync(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(_categoryRepository.GetCategory(id));
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Delete(Category category)
        {
            await _categoryRepository.DeleteCategoryAsync(category.Id);
            return RedirectToAction("Index");
        }
    }
}