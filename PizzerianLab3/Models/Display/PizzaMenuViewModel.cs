using System.Collections.Generic;

namespace PizzerianLab3.Models.Display
{
    public class PizzaMenuViewModel
    {
        public PizzaMenuViewModel()
        {
            PizzaIngredients = new List<IngredientViewModel>();
            ExtraIngredients = new List<ExtraIngredientViewModel>();
        }

        public int MenuNumber { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public IEnumerable<IngredientViewModel> PizzaIngredients { get; set; }
        public ICollection<ExtraIngredientViewModel> ExtraIngredients { get; set; }
    }
}