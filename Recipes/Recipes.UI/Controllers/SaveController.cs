using Microsoft.AspNetCore.Mvc;
using Recipes.Core;
using Recipes.Repos;

namespace Recipes.UI.Controllers
{
    public class SaveController : Controller
    {
        private readonly SaveRepository saveRepository;
        private readonly InfoDishRepository InfodishRepository;
        private readonly UsersRepository usersRepository;
        public SaveController (SaveRepository saveRepository, InfoDishRepository InfodishRepository, UsersRepository usersRepository)
        {
            this.saveRepository = saveRepository;
            this.InfodishRepository = InfodishRepository;
            this.usersRepository = usersRepository;
        }
        public async Task<IActionResult> Create (int id)
        {
            var user = await usersRepository.GetCurrentUser();
            var infoDish =  InfodishRepository.GetInfoDish(id);
            var list = new List<InfoDish>();
            list.Add(infoDish);
            await saveRepository.Create(user, infoDish);
            ViewBag.Saves = await saveRepository.GetAllSave(user); 
            return View("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var save = saveRepository.GetSave(id);
            await saveRepository.Delete(id);

            return View("Index");
        }

        public async Task<IActionResult> Index()
        {
            var user = await usersRepository.GetCurrentUser();
            return View(await saveRepository.GetAllSave(user));
        }

       
    }
}
