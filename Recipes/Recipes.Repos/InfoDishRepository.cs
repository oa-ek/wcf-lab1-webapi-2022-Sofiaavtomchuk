using Recipes.Core;
using Recipes.Repos.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using RecipesBlazorApp.Shared.Dtos.InfoDish;

namespace Recipes.Repos
{
    public class InfoDishRepository
    {
        private readonly RecipesContext _ctx;
        public InfoDishRepository(RecipesContext _ctx)
        {
            this._ctx = _ctx;
        }

        public async Task<InfoDish> AddInfoDishAsync(InfoDish infoDish)
        {
            _ctx.InfoDishes.Add(infoDish);
            await _ctx.SaveChangesAsync();
            return _ctx.InfoDishes.Include(x => x.Categories).FirstOrDefault(x => x.Title == infoDish.Title);
            
        }

        public InfoDish GetInfoDish(int id)
        {
            return _ctx.InfoDishes.Include(x => x.Categories).FirstOrDefault(x => x.Id == id);
        }

        public List<InfoDish> GetInfoDishes()
        {
            var infoDishList = _ctx.InfoDishes.Include(x => x.Categories).ToList();
            return infoDishList;
        }
        public List<InfoDishDto> GetInfoDishesDto()
        {
           
            var infoDishList = _ctx.InfoDishes.Include(x => x.Categories).ToList();
            var infoDishListDto = new List<InfoDishDto>();
            foreach (var infoDish in infoDishList)
            {
                infoDishListDto.Add(new InfoDishDto
                {
                    Id = infoDish.Id,
                    Title = infoDish.Title,
                    IconPath = infoDish.IconPath,
                    Difficulty = infoDish.Difficulty,
                    CookingTime = infoDish.CookingTime,
                    Ingredients1 = infoDish.Ingredients1,
                    Ingredients2 = infoDish.Ingredients2,
                    Ingredients3 = infoDish.Ingredients3,
                    Preparation1 = infoDish.Preparation1,
                    Preparation2 = infoDish.Preparation2, 
                    Categories = infoDish.Categories.NameCategory
                });
            }
            return infoDishListDto;
        }

        public async Task DeleteInfoDishAsync(int id)
        {
            _ctx.Remove(GetInfoDish(id));
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateInfoDishAsync(InfoDish updatedInfoDish)
        {
            var infoDish = _ctx.InfoDishes.FirstOrDefault(x => x.Id == updatedInfoDish.Id);
            infoDish.Title = updatedInfoDish.Title;
            infoDish.IconPath = updatedInfoDish.IconPath;
            infoDish.Difficulty = updatedInfoDish.Difficulty;
            infoDish.CookingTime = updatedInfoDish.CookingTime;
            infoDish.Ingredients1 = updatedInfoDish.Ingredients1;
            infoDish.Ingredients2 = updatedInfoDish.Ingredients2;
            infoDish.Ingredients3 = updatedInfoDish.Ingredients3;
            infoDish.Ingredients4 = updatedInfoDish.Ingredients4;
            infoDish.Ingredients5 = updatedInfoDish.Ingredients5;
            infoDish.Ingredients6 = updatedInfoDish.Ingredients6;
            infoDish.Ingredients7 = updatedInfoDish.Ingredients7;
            infoDish.Ingredients8 = updatedInfoDish.Ingredients8;
            infoDish.Preparation1 = updatedInfoDish.Preparation1;
            infoDish.Preparation2 = updatedInfoDish.Preparation2;
            infoDish.Preparation3 = updatedInfoDish.Preparation3;
            infoDish.Preparation4 = updatedInfoDish.Preparation4;
            infoDish.Categories = updatedInfoDish.Categories;
            await _ctx.SaveChangesAsync();
        }

        public async Task<InfoDishCreateDto> GetInfoDishDto(int id)
        {
            var i = await _ctx.InfoDishes.Include(x => x.Categories).FirstAsync(x => x.Id == id);

            var infoDishDto = new InfoDishCreateDto
            {
                Id = i.Id,
                Title = i.Title,
                IconPath = i.IconPath,
                Difficulty = i.Difficulty,
                CookingTime = i.CookingTime,
                Ingredients1 = i.Ingredients1,
                Ingredients2 = i.Ingredients2,
                Ingredients3 = i.Ingredients3,
                Ingredients4 = i.Ingredients4,
                Ingredients5 = i.Ingredients5,
                Ingredients6 = i.Ingredients6,
                Ingredients7 = i.Ingredients7,
                Ingredients8 = i.Ingredients8,
                Preparation1 = i.Preparation1,
                Preparation2 = i.Preparation2,
                Preparation3 = i.Preparation3,
                Preparation4 = i.Preparation4,
                Categories = i.Categories.NameCategory,
            };
            return infoDishDto;
        }

        public async Task UpdateAsync(InfoDishCreateDto model, string categories)
        {
            var infoDish = _ctx.InfoDishes.Include(x => x.Categories).FirstOrDefault(x => x.Id == model.Id);
            if (infoDish.Title != model.Title)
                infoDish.Title = model.Title;
            if (infoDish.IconPath != model.IconPath)
                infoDish.IconPath = model.IconPath;
            if (infoDish.Difficulty != model.Difficulty)
                infoDish.CookingTime = model.CookingTime;
            if (infoDish.Ingredients1 != model.Ingredients1)
                infoDish.Ingredients1 = model.Ingredients1;
            if (infoDish.Ingredients2 != model.Ingredients2)
                infoDish.Ingredients2 = model.Ingredients2;
            if (infoDish.Ingredients3 != model.Ingredients3)
                infoDish.Ingredients3 = model.Ingredients3;
            if (infoDish.Ingredients4 != model.Ingredients4)
                infoDish.Ingredients4 = model.Ingredients4;
            if (infoDish.Ingredients5 != model.Ingredients5)
                infoDish.Ingredients5 = model.Ingredients5;
            if (infoDish.Ingredients6 != model.Ingredients6)
                infoDish.Ingredients6 = model.Ingredients6;
            if (infoDish.Ingredients7 != model.Ingredients7)
                infoDish.Ingredients7 = model.Ingredients7;
            if (infoDish.Ingredients8 != model.Ingredients8)
                infoDish.Ingredients8 = model.Ingredients8;
            if (infoDish.Preparation1 != model.Preparation1)
                infoDish.Preparation1 = model.Preparation1;
            if (infoDish.Preparation2 != model.Preparation2)
                infoDish.Preparation2 = model.Preparation2;
            if (infoDish.Preparation3 != model.Preparation3)
                infoDish.Preparation3 = model.Preparation3;
            if (infoDish.Preparation4 != model.Preparation4)
                infoDish.Preparation4 = model.Preparation4;
            if (infoDish.Categories.NameCategory != model.Categories)
                infoDish.Categories = _ctx.Categories.FirstOrDefault(x => x.NameCategory == categories);
            _ctx.SaveChanges();
        }


        public async Task<InfoDishCreateDto> GetInfoDishById(int id)
        {
            var infoDish = await _ctx.InfoDishes.FindAsync(id);
            if(infoDish != null)
            {
                var Details = new InfoDishCreateDto()
                {
                    Id = infoDish.Id,
                    Title = infoDish.Title,
                    IconPath = infoDish.IconPath,
                    Difficulty = infoDish.Difficulty,
                    CookingTime = infoDish.CookingTime,
                    Ingredients1 = infoDish.Ingredients1,
                    Ingredients2 = infoDish.Ingredients2,
                    Ingredients3 = infoDish.Ingredients3,
                    Ingredients4 = infoDish.Ingredients4,
                    Ingredients5 = infoDish.Ingredients5,
                    Ingredients6 = infoDish.Ingredients6,
                    Ingredients7 = infoDish.Ingredients7,
                    Ingredients8 = infoDish.Ingredients8,
                    Preparation1 = infoDish.Preparation1,
                    Preparation2 = infoDish.Preparation2,
                    Preparation3 = infoDish.Preparation3,
                    Preparation4 = infoDish.Preparation4,
                    Categories = infoDish.Categories.NameCategory,
                };
                return Details;
            }
            return null;
        }
    }
}