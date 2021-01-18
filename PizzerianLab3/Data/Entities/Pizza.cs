using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzerianLab3.Data.Entities
{
    public class Pizza
    {
        public Pizza()
        {
            PizzaIngredients = new List<Ingredient>();
            ExtraIngredients = new List<Ingredient>();
        }

        public Guid Id { get; set; }
        public int MenuNumber { get; set; }
        public string Name { get; set; }
        public double BasePrice { get; set; }
        public double TotalPrice => BasePrice + ExtraIngredients.Select(x => x.Price).Sum();
        public virtual ICollection<Ingredient> PizzaIngredients { get; set; }
        public virtual ICollection<Ingredient> ExtraIngredients { get; set; }
    }
}