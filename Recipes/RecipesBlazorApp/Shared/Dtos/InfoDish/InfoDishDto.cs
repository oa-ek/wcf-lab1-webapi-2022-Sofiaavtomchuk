using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace RecipesBlazorApp.Shared.Dtos.InfoDish
{
        public class InfoDishDto
        {
            
            public int? Id { get; set; }

          [Required(ErrorMessage = "Введіть назву нового рецепту")]
          [MinLength(5, ErrorMessage = "Мінімальна довжина поля 5 символів")]
           public string? Title { get; set; }

            public string? IconPath { get; set; }

            public string? Difficulty { get; set; }

            public string? CookingTime { get; set; }

            public string? Ingredients1 { get; set; }
            public string? Ingredients2 { get; set; }
            public string? Ingredients3 { get; set; }

            public string? Preparation1 { get; set; }
            public string? Preparation2 { get; set; }

            public string? Categories { get; set; }

        }
    

}
