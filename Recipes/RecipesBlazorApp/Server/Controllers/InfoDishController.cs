using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Recipes.Core;
using Recipes.Repos.Dto;
using Recipes.Repos;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        /// <summary>
        /// Method returnes list of dishes from database
        /// </summary>
        /// 
        [HttpGet]
        public List<InfoDish>? GetInfoDish()
        {
            var infoDish = infoDishRepository.GetInfoDishes();
            //var category = categoryRepository.GetCategory();
            return infoDish;

        }

        /// <summary>
        /// Method creates dish and adds it to db
        /// </summary>
        /// <param name="categories">gives the name of category, which we need to create dish</param>
        [HttpPost]
        public async Task Create(InfoDishCreateDto infoDishCreateDto, string categories)
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
        }

        /// <summary>
        /// Method edits dish from database
        /// </summary>
        /// <param name="categories"> name of category, which we need to edit</param>
        [HttpPut("{id}/edit")]
        public async Task Edit(InfoDishCreateDto model, string categories)
        {
            await infoDishRepository.UpdateAsync(model, categories);
        }

        /// <summary>
        /// Method deletes dish from db by id
        /// </summary>
        /// <param name="id">id of deleting dish</param>
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await infoDishRepository.DeleteInfoDishAsync(id);
        }

        /// <summary>
        /// Method details dish from db by id
        /// </summary>
        /// <param name="id">id for opening details dish</param>
        [HttpGet("{id}/details")]
        public async Task<InfoDishCreateDto> Details(int id)
        {
            var info = await infoDishRepository.GetInfoDishDto(id);
            return info;
        }

        /// <summary>
        /// Method search needed dishes by search string in db and returns the list of them
        /// </summary>
        [HttpGet]
        [Route("Search")]
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
