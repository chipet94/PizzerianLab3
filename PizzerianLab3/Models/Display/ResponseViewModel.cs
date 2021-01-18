using System;
using System.Collections.Generic;

namespace PizzerianLab3.Models.Display
{
    public class ResponseViewModel
    {
        public Guid OrderId { get; set; }
        public IEnumerable<PizzaViewModel> Pizzas { get; set; } = new List<PizzaViewModel>();
        public IEnumerable<SodaViewModel> Sodas { get; set; } = new List<SodaViewModel>();
        public double TotalPrice { get; set; }
    }
}