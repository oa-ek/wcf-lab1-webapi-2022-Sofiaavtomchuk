using Recipes.Core;
using Recipes.Repos;
using Recipes.Repos.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System.Drawing.Drawing2D;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using System;

/*namespace Recipes.UI.Controllers
{
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

        [HttpGet]
        public IActionResult Index()
        {
            var infoDish = infoDishRepository.GetInfoDishes();
            return View(infoDish);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = categoryRepository.GetCategories();
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
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
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Categories = categoryRepository.GetCategories();
            return View(await infoDishRepository.GetInfoDishDto(id));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
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

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await infoDishRepository.GetInfoDishDto(id));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await infoDishRepository.DeleteInfoDishAsync(id);
            return RedirectToAction("Index");
        }
        public async Task<ViewResult> Details(int id)
        {
            var info = await infoDishRepository.GetInfoDishDto(id);
            return View(info);
        }

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
}*/