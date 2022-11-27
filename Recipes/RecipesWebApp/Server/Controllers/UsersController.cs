using Microsoft.AspNetCore.Mvc;
using Recipes.Core;
using Recipes.Repos.Dto;
using Recipes.Repos;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace RecipesWebApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly UsersRepository usersRepository;
        public UsersController(UsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        // Post: Accommodation/Index
        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            return View(await usersRepository.GetUsersAsync());
        }

        // GET: Accommodation/Create
        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // GET: Accommodation/Create
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(UserCreateDto model)
        {
            if (ModelState.IsValid)
            {
                User user = await usersRepository.CreateUserAsync(model.FirstName, model.LastName, model.Password, model.Email);
                return RedirectToAction("Edit", "Users", new { id = user.Id });
            }
            return View(model);
        }

        // Post: Accommodation/Delete
        [HttpGet]
        [Route("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            return View(await usersRepository.GetUserAsync(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Delete")]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            await usersRepository.DeleteUserAsync(id);
            return RedirectToAction("Index");
        }

        // Post: Accommodation/Edit
        [HttpGet]
        [Route("Edit")]
        public async Task<IActionResult> Edit(string id)
        {
            ViewBag.Roles = await usersRepository.GetRolesAsync();
            return View(await usersRepository.GetUserAsync(id));
        }

        // POST: Accommodation/Edit
        [NonAction]
        public async Task<IActionResult> Edit(UserReadDto model, string[] roles)
        {
            if (ModelState.IsValid)
            {
                await usersRepository.UpdateAsync(model, roles);
                return RedirectToAction("Index");
            }
            ViewBag.Roles = await usersRepository.GetRolesAsync();
            return View(model);
        }
    }
}
