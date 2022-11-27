using Microsoft.AspNetCore.Mvc;
using Recipes.Core;
using Recipes.Repos;

namespace RecipesWebApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaveController : Controller
    {
        private readonly SaveRepository saveRepository;
        private readonly InfoDishRepository InfodishRepository;
        private readonly UsersRepository usersRepository;
        public SaveController(SaveRepository saveRepository, InfoDishRepository InfodishRepository, UsersRepository usersRepository)
        {
            this.saveRepository = saveRepository;
            this.InfodishRepository = InfodishRepository;
            this.usersRepository = usersRepository;
        }

        // GET: Accommodation/Create
        [HttpGet]
        [Route("Create")]
        public async Task<IActionResult> Create(int id)
        {
            var user = await usersRepository.GetCurrentUser();
            var infoDish = InfodishRepository.GetInfoDish(id);
            var list = new List<InfoDish>();
            list.Add(infoDish);
            await saveRepository.Create(user, infoDish);
            ViewBag.Saves = await saveRepository.GetAllSave(user);
            return View("Index");
        }
        // Post: Accommodation/Delete
        [HttpGet]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var save = saveRepository.GetSave(id);
            await saveRepository.Delete(id);

            return View("Index");
        }

        // Post: Accommodation/Index
        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var user = await usersRepository.GetCurrentUser();
            return View(await saveRepository.GetAllSave(user));
        }
    }
}
