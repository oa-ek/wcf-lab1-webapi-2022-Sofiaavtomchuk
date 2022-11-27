using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recipes.Core;

namespace RecipesWebApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : Controller
    {
        private readonly RecipesContext _context;

        public SearchController(RecipesContext context)
        {
            _context = context;
        }

        // GET: api/<SearchController>
        [HttpGet("{title}/{difficulty}/{cookingTime}")]
        public async Task<IEnumerable<InfoDish>> Get(string title, string difficulty, string cookingTime)
        {
            IEnumerable<InfoDish> dishes = null;

            string Difficulty = difficulty;
            string CookingTime = cookingTime;
            //Category Categories = categories;

            if (!title.Equals(""))
            {
                dishes = await _context.InfoDishes
                    .Where(o => o.Title == title && o.CookingTime == cookingTime && o.Difficulty == difficulty)
                    .Select(o => new InfoDish
                    {
                        Id = o.Id,
                        Title = o.Title,
                        IconPath = o.IconPath,
                        Difficulty = o.Difficulty,
                        CookingTime = o.CookingTime,
                        Ingredients1 = o.Ingredients1,
                        Ingredients2 = o.Ingredients2,
                        Ingredients3 = o.Ingredients3,
                        Ingredients4 = o.Ingredients4,
                        Ingredients5 = o.Ingredients5,
                        Ingredients6 = o.Ingredients6,
                        Ingredients7 = o.Ingredients7,
                        Ingredients8 = o.Ingredients8,
                        Preparation1 = o.Preparation1,
                        Preparation2 = o.Preparation2,
                        Preparation3 = o.Preparation3,
                        Preparation4 = o.Preparation4,
                        //Categories = o.Categories

                    })
                    .ToListAsync();
            }

            return dishes;
        }
    }
}
