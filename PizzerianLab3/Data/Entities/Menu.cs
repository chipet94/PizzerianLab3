using System.Collections.Generic;

namespace PizzerianLab3.Data.Entities
{
    public class Menu : Entity
    {
        public Menu()
        {
            PizzaMenu = new List<Pizza>();
            SodaMenu = new List<Soda>();
            IngredientMenu = new List<Ingredient>();
        }

        public virtual ICollection<Pizza> PizzaMenu { get; set; }
        public virtual ICollection<Soda> SodaMenu { get; set; }
        public virtual ICollection<Ingredient> IngredientMenu { get; set; }
    }
}