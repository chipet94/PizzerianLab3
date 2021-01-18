using System;
using System.Collections.Generic;

namespace PizzerianLab3.Models.Display
{
    public class PizzaViewModel
    {
        public PizzaViewModel()
        {
            PizzaIngredients = new List<IngredientViewModel>();
            ExtraIngredients = new List<ExtraIngredientViewModel>();
        }

        public Guid PizzaId { get; set; }
        public int MenuNumber { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public IEnumerable<IngredientViewModel> PizzaIngredients { get; set; }
        public IEnumerable<ExtraIngredientViewModel> ExtraIngredients { get; set; }
    }
}