using Microsoft.AspNetCore.Mvc;
using Recipes.Core;
using Recipes.Repos;

namespace RecipesBlazorApp.Server.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _categoryRepository;
        public CategoryController(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        /// <summary>
        /// Method takes brand from db
        /// </summary>
        /// <param name="id">id of searching brand</param>
        /// <returns>brand from db</returns>
        [HttpGet("{id}")]
        public Category GetCategory(int id)
        {
            return _categoryRepository.GetCategory(id);
        }

        /// <summary>
        /// Method creates category and adds it to db
        /// </summary>
        /// <returns>created category from db</returns>
        [HttpPost]
        [Route("Create")]
        public async Task<Category> Create(Category category)
        {   
                var createdCategory = await _categoryRepository.AddCategoryAsync(category);
                return createdCategory;
        }
        /// <summary>
        /// Method edits category from database
        /// </summary>
        /// <param name="id"> id category, which we need to edit</param>
        [HttpPut("{id}/edit")]
        public async Task Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryRepository.UpdateCategoryAsync(category);
                //return RedirectToAction("Index");
            }
            //return View(category);
        }
        /// <summary>
        /// Method deletes category from db by id
        /// </summary>
        /// <param name="id">id of deleting category</param>
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _categoryRepository.DeleteCategoryAsync(id);
            //return RedirectToAction("Index");
        }
    }
}
