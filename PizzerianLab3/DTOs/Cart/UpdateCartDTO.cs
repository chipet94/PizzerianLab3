using System;
using System.Collections.Generic;
using PizzerianLab3.DTOs.ModelDTO;

namespace PizzerianLab3.DTOs.Cart
{
    public class UpdateCartDTO
    {
        public Guid PizzaId { get; set; }
        public List<ExtraIngredientDTO> ExtraIngredients { get; set; }
    }
}