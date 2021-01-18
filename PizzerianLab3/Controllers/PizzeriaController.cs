using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzerianLab3.Data;
using PizzerianLab3.Extensions.Models;
using PizzerianLab3.Models.Display;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PizzerianLab3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzeriaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PizzeriaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/<OrderController>
        [HttpGet]
        [SwaggerOperation(Summary = "Get menu")]
        public async Task<IActionResult> Get()
        {
            var menu = await _context.Menus
                .Select(x => new
                {
                    x.PizzaMenu,
                    x.SodaMenu,
                    x.IngredientMenu
                })
                .FirstOrDefaultAsync();

            var menuDisplayModel = new MenuViewModel()
            {   
                Pizzas = menu.PizzaMenu.ToMenuViewModels(),
                Sodas = menu.SodaMenu.ToViewModels(),
                ExtraIngredients = menu.IngredientMenu.ToExtrasViewModels()

            };
            return Ok(menuDisplayModel);
        }
    }
}