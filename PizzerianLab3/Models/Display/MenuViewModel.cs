using System.Collections.Generic;

namespace PizzerianLab3.Models.Display
{
    public class MenuViewModel
    {
        public IEnumerable<PizzaMenuViewModel> Pizzas { get; set; }
        public IEnumerable<SodaViewModel> Sodas { get; set; }
        public IEnumerable<ExtraIngredientViewModel> ExtraIngredients { get; set; }
    }
}