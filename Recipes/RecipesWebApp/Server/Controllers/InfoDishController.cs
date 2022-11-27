using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Recipes.Core;
using Recipes.Repos.Dto;
using Recipes.Repos;
using System.Diagnostics;

namespace RecipesWebApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InfoDishController : Controller
    {
        private readonly InfoDishRepository infoDishRepository;
        private readonly CategoryRepository categoryRepository;
        private readonly RecipesContext rContext;

        public InfoDishController(InfoDishRepository infoDishRepository, CategoryRepository categoryRepository)
        {
            this.infoDishRepository = infoDishRepository;
            this.categoryRepository = categoryRepository;

        }

        // GET: Accommodation
        [HttpGet, ActionName("Index")]
        [Route("Index")]
        public IActionResult Index()
        {
            var infoDish = infoDishRepository.GetInfoDishes();
            return View(infoDish);
        }

        // GET: Accommodation/Create
        [NonAction]
        public IActionResult Create()
        {
            ViewBag.Categories = categoryRepository.GetCategories();
            return View();
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(InfoDishCreateDto infoDishCreateDto, string categories)
        {
            ViewBag.Categories = categoryRepository.GetCategories();
            if (ModelState.IsValid)
            {

                var category = categoryRepository.GetCategoryByName(categories);
                if (category == null)
                {
                    category = new Category() { NameCategory = categories };
                    category = await categoryRepository.AddCategoryAsync(category);
                }


                var infoDish = await infoDishRepository.AddInfoDishAsync(new InfoDish()
                {
                    Title = infoDishCreateDto.Title,
                    IconPath = infoDishCreateDto.IconPath,
                    Difficulty = infoDishCreateDto.Difficulty,
                    CookingTime = infoDishCreateDto.CookingTime,
                    Ingredients1 = infoDishCreateDto.Ingredients1,
                    Ingredients2 = infoDishCreateDto.Ingredients2,
                    Ingredients3 = infoDishCreateDto.Ingredients3,
                    Ingredients4 = infoDishCreateDto.Ingredients4,
                    Ingredients5 = infoDishCreateDto.Ingredients5,
                    Ingredients6 = infoDishCreateDto.Ingredients6,
                    Ingredients7 = infoDishCreateDto.Ingredients7,
                    Ingredients8 = infoDishCreateDto.Ingredients8,
                    Preparation1 = infoDishCreateDto.Preparation1,
                    Preparation2 = infoDishCreateDto.Preparation2,
                    Preparation3 = infoDishCreateDto.Preparation3,
                    Preparation4 = infoDishCreateDto.Preparation4,
                    Categories = category,
                });

                return RedirectToAction("Edit", "InfoDish", new { id = infoDish.Id });
            }
            return View(infoDishCreateDto);
        }

        // Post: Accommodation/Edit
        [NonAction]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Categories = categoryRepository.GetCategories();
            return View(await infoDishRepository.GetInfoDishDto(id));
        }

        // POST: Accommodation/Edit
        [HttpGet]
        [Route("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(InfoDishCreateDto model, string categories)
        {
            if (ModelState.IsValid)
            {
                await infoDishRepository.UpdateAsync(model, categories);
                return RedirectToAction("Index");
            }
            ViewBag.Categories = categoryRepository.GetCategories();
            return View(model);
        }

        // GET: Accommodation/Delete
        [HttpGet]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await infoDishRepository.GetInfoDishDto(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await infoDishRepository.DeleteInfoDishAsync(id);
            return RedirectToAction("Index");
        }

        // Post: Accommodation/Details
        [HttpPost, ActionName("Details")]
        [Route("Details")]
        public async Task<ViewResult> Details(int id)
        {
            var info = await infoDishRepository.GetInfoDishDto(id);
            return View(info);
        }

        // Post: Accommodation/Search
        [HttpPost, ActionName("Search")]
        public async Task<IActionResult> Search(string title, string difficulty, string cookingTime)
        {
            HttpClient client = new();

            string path = this.Request.Scheme + "://" + this.Request.Host.Value + "/api/search/" + title + "/" + difficulty + "/" + cookingTime;
            Debug.WriteLine("Search API path: " + path);

            IEnumerable<InfoDish> dishes = null;

            HttpResponseMessage response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                dishes = JsonConvert.DeserializeObject<IEnumerable<InfoDish>>(await response.Content.ReadAsStringAsync());

                ViewBag.Search = true;
            }

            return View("Index", dishes);
        }
    }
}
