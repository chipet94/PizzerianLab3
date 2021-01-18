using System;
using System.Collections.Generic;
using System.Linq;
using PizzerianLab3.Data.Entities;
using PizzerianLab3.Models.Display;

namespace PizzerianLab3.Extensions.Models
{
    public static class IngredientExtensions
    {
        public static IngredientViewModel ToViewModel(this Ingredient ingredient)
        {
            return new()
            {
                Name = ingredient.Name
            };
        }

        public static IEnumerable<IngredientViewModel> ToViewModels(this IEnumerable<Ingredient> ingredients)
        {
            return ingredients.Select(x => x.ToViewModel());
        }

        public static ExtraIngredientViewModel ToExtrasViewModel(this Ingredient ingredient)
        {
            return new()
            {
                MenuNumber = ingredient.MenuNumber,
                Name = ingredient.Name,
                Price = ingredient.Price
            };
        }

        public static IEnumerable<ExtraIngredientViewModel> ToExtrasViewModels(this IEnumerable<Ingredient> ingredients)
        {
            return ingredients.Select(x => x.ToExtrasViewModel());
        }

        public static Ingredient CopyToNew(this Ingredient ingredient)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                Price = ingredient.Price,
                IngredientOption = ingredient.IngredientOption,
                MenuNumber = ingredient.MenuNumber
            };
        }
    }
}