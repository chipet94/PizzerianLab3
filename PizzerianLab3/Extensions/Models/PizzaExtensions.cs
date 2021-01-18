using System;
using System.Collections.Generic;
using System.Linq;
using PizzerianLab3.Data.Entities;
using PizzerianLab3.Models.Display;

namespace PizzerianLab3.Extensions.Models
{
    public static class PizzaExtensions
    {
        public static PizzaViewModel ToViewModel(this Pizza pizza)
        {
            return new()
            {
                PizzaId = pizza.Id,
                Price = pizza.BasePrice,
                Name = pizza.Name,
                MenuNumber = pizza.MenuNumber,
                ExtraIngredients = pizza.ExtraIngredients?.ToExtrasViewModels(),
                PizzaIngredients = pizza.PizzaIngredients?.ToViewModels()
            };
        }
        public static PizzaMenuViewModel ToMenuViewModel(this Pizza pizza)
        {
            return new()
            {
                Price = pizza.BasePrice,
                Name = pizza.Name,
                MenuNumber = pizza.MenuNumber,
                PizzaIngredients = pizza.PizzaIngredients?.ToViewModels()
            };
        }
        public static IEnumerable<PizzaMenuViewModel> ToMenuViewModels(this IEnumerable<Pizza> pizzas)
        {
            return pizzas.Select(x => x.ToMenuViewModel());
        }

        public static IEnumerable<PizzaViewModel> ToViewModels(this IEnumerable<Pizza> pizzas)
        {
            return pizzas.Select(x => x.ToViewModel());
        }

        public static Pizza CopyToNew(this Pizza pizza)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                MenuNumber = pizza.MenuNumber,
                Name = pizza.Name,
                ExtraIngredients = pizza.ExtraIngredients?.Select(x => x.CopyToNew()).ToList(),
                PizzaIngredients = pizza.PizzaIngredients?.Select(x => x.CopyToNew()).ToList(),
                BasePrice = pizza.BasePrice
            };
        }
    }
}