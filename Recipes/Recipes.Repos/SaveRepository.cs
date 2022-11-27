using Microsoft.EntityFrameworkCore;
using Recipes.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Repos
{
    public class SaveRepository
    {
        private readonly RecipesContext _ctx;
        public SaveRepository(RecipesContext _ctx)
        {
            this._ctx = _ctx;
        }
        public async Task<Save> GetSave(int id)
        {
            return await _ctx.Saves.FirstAsync(x => x.Id == id);
        }
        public async Task<List<Save>> GetAllSave(User user)
        {
            return await _ctx.Saves.Where(x=>x.user==user).ToListAsync();
        }
        public async Task Create(User user, InfoDish infoDish)
        {
            var newCt = new Save
            {
                user = user,
                infoDish = infoDish,
            };

           //newCt.infoDish.Add(infoDish);
            await _ctx.Saves.AddAsync(newCt);
            await _ctx.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var catalog = await GetSave(id);
            _ctx.Saves.Remove(catalog);
            await _ctx.SaveChangesAsync();
        }
    }
}
