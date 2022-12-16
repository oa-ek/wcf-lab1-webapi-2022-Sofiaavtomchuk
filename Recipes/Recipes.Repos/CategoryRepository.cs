using Recipes.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Repos
{
    public class CategoryRepository
    {
        private readonly RecipesContext _ctx;

        public CategoryRepository(RecipesContext _ctx)
        {
            this._ctx = _ctx;
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            _ctx.Categories.Add(category);
            await _ctx.SaveChangesAsync();
            return _ctx.Categories.FirstOrDefault(x => x.NameCategory == category.NameCategory);
        }

        public Category GetCategory(int id)
        {
            return _ctx.Categories.FirstOrDefault(x => x.Id == id);
        }
        public Category GetCategoryByName(string name)
        {
            return _ctx.Categories.FirstOrDefault(x => x.NameCategory == name);
        }

        public List<Category> GetCategories(int id)
        {
            var categoryList = _ctx.Categories.ToList();
            return categoryList;
        }

        public async Task DeleteCategoryAsync(int id)
        {
            _ctx.Remove(GetCategory(id));
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category updatedCategory)
        {
            var category = _ctx.Categories.FirstOrDefault(x => x.Id == updatedCategory.Id);
            category.NameCategory = updatedCategory.NameCategory;
            await _ctx.SaveChangesAsync();
        }

        public InfoDish GetInfoDish(int id)
        {
            throw new NotImplementedException();
        }
    }
}