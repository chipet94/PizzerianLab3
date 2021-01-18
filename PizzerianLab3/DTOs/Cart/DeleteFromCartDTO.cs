using System.Collections.Generic;

namespace PizzerianLab3.DTOs.Cart
{
    public class DeleteFromCartDTO
    {
        public List<string> PizzasIds { get; set; }
        public List<int> SodasMenuNumber { get; set; }
    }
}