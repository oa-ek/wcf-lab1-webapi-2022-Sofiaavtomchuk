using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesBlazorApp.Shared.Dtos.InfoDish
{
    public class Categories
    {
        public int? Id { get; set; }

        public string? NameCategory { get; set; }

    }
}
