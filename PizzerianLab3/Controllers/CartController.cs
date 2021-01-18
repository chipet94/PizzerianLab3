using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzerianLab3.Data;
using PizzerianLab3.DTOs.Cart;
using PizzerianLab3.Extensions.Models;
using PizzerianLab3.Models.Display;
using PizzerianLab3.Singletons;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PizzerianLab3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartSingleton _cart;
        private readonly AppDbContext _context;

        public CartController(AppDbContext context, CartSingleton cart)
        {
            _context = context;
            _cart = cart;
        }

        // GET: api/<ValuesController>

        // GET api/<ValuesController>/5
        [HttpGet]
        [SwaggerOperation(Summary = "Get current shopping cart")]
        public ActionResult GetCartContent()
        {
            if (_cart.Order.IsEmpty)
                return Ok("Your cart is empty");

            var viewCartContent = new ResponseViewModel
            {
                Pizzas = _cart.Order.Pizzas.ToViewModels(),
                Sodas = _cart.Order.Sodas.ToViewModels(),
                TotalPrice = _cart.Order.TotalPrice
            };

            return Ok(viewCartContent);
        }

        // POST api/<ValuesController>
        [HttpPost]
        [SwaggerOperation(Summary = "Place item to shopping cart")]
        public async Task<IActionResult> Post([FromBody] AddItemToCartDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var menu = await _context.Menus.FirstOrDefaultAsync();

            var pizzaRecipe = menu.PizzaMenu.FirstOrDefault(x => x.MenuNumber == request.PizzaMenuNumber);
            var sodaOption = menu.SodaMenu.FirstOrDefault(x => x.MenuNumber == request.SodaMenuNumber);

            if (pizzaRecipe == null && sodaOption == null)
                return BadRequest("No items seleceted.");

            if (pizzaRecipe != null)
                _cart.Order.Pizzas.Add(pizzaRecipe.CopyToNew());

            if (sodaOption != null)
                _cart.Order.Sodas.Add(sodaOption.CopyToNew());


            return Ok(new
            {
                pizza = pizzaRecipe?.Name,
                soda = sodaOption?.Name
            });
        }

        // PUT api/<ValuesController>/5
        [HttpPut]
        [SwaggerOperation(Summary = "Update shopping cart")]
        public async Task<IActionResult> Put([FromBody] UpdateCartDTO request)
        {
            if (!ModelState.IsValid)
                BadRequest("Bad request");

            var pizzaToModify = _cart.Order.Pizzas.FirstOrDefault(x => x.Id == request.PizzaId);

            if (pizzaToModify == null)
                return BadRequest("Could not find pizza id");

            if (request.ExtraIngredients.Any())
            {
                if (request.ExtraIngredients.Any(x => x.MenuNumber < 1 && x.MenuNumber > 11))
                    return BadRequest("Request contains tainded ingredient id, please re-check your order.");

                var menu = await _context.Menus.FirstOrDefaultAsync();

                foreach (var ingredientOption in request.ExtraIngredients
                    .Select(ingredient => menu.IngredientMenu
                        .FirstOrDefault(x => x.MenuNumber == ingredient.MenuNumber)))
                    pizzaToModify.ExtraIngredients.Add(ingredientOption.CopyToNew());
            }

            return Ok(request);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete]
        [SwaggerOperation(Summary = "Remove items from shopping cart")]
        public IActionResult Delete([FromBody] DeleteFromCartDTO request)
        {
            if (!ModelState.IsValid)
                BadRequest("Bad request");

            if (!string.IsNullOrWhiteSpace(request.PizzasIds.FirstOrDefault()))
                if (request.PizzasIds.FirstOrDefault() != "string")
                    foreach (var pizzaInCart in request.PizzasIds.Select(pizzaId => _cart.Order.Pizzas
                        .FirstOrDefault(x => x.Id.ToString() == pizzaId)))
                        if (pizzaInCart != null)
                            _cart.Order.Pizzas.Remove(pizzaInCart);
                        else
                            return BadRequest("There's no pizza with that Id in your shopping cart.");

            if (request.SodasMenuNumber.Count > 0)
                foreach (var sodaInCart in request.SodasMenuNumber
                    .Select(sodaId => _cart.Order.Sodas.FirstOrDefault(x => x.MenuNumber == sodaId)))
                    if (sodaInCart != null)
                        _cart.Order.Sodas.Remove(sodaInCart);
                    else
                        return BadRequest("There's no soda with that menu number in your shopping cart.");
            else
                return BadRequest("Please type a valid Soda id");

            return Ok(request);
        }
    }
}