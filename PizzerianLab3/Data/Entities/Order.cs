using System.Collections.Generic;
using System.Linq;

namespace PizzerianLab3.Data.Entities
{
    public class Order : Entity
    {
        public Order()
        {
            Pizzas = new List<Pizza>();
            Sodas = new List<Soda>();
        }

        public virtual ICollection<Pizza> Pizzas { get; set; }
        public virtual ICollection<Soda> Sodas { get; set; }

        public double TotalPrice => Pizzas.Select(x => x.TotalPrice).Sum() + Sodas.Select(x => x.Price).Sum();
        public bool IsEmpty => !Pizzas.Any() && !Sodas.Any();
    }
}