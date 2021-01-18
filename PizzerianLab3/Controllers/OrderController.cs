using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzerianLab3.Data;
using PizzerianLab3.Data.Entities;
using PizzerianLab3.DTOs.Order;
using PizzerianLab3.Extensions.Models;
using PizzerianLab3.Models.Display;
using PizzerianLab3.Singletons;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PizzerianLab3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly CartSingleton _cart;
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context, CartSingleton cart)
        {
            _context = context;
            _cart = cart;
        }

        // GET api/<OrderController>/5
        [HttpGet]
        [SwaggerOperation(Summary = "Get all orders in progress")]
        public async Task<IActionResult> Get()
        {
            // All non-completed orders
            var orders = await _context.Orders
                .Where(x => x.IsActive)
                .Select(x => new
                {
                    x.Id,
                    x.Pizzas,
                    x.Sodas,
                    x.TotalPrice
                }).ToListAsync();

            var viewActiveOrders = new List<ResponseViewModel>();
            foreach (var order in orders)
            {
                var viewActiveOrderModel = new ResponseViewModel
                {
                    OrderId = order.Id,
                    Pizzas = order.Pizzas.ToViewModels(),
                    Sodas = order.Sodas.ToViewModels(),
                    TotalPrice = order.TotalPrice
                };
                viewActiveOrders.Add(viewActiveOrderModel);
            }

            return Ok(viewActiveOrders);
        }

        // POST api/<OrderController>
        [HttpPost]
        [SwaggerOperation(Summary = "Place order with items from customer cart")]
        public async Task<IActionResult> Post()
        {
            if (!ModelState.IsValid)
                BadRequest("Bad request");

            var itemsInCart = _cart.Order;
            var order = new Order
            {
                Id = Guid.NewGuid(),
                Pizzas = itemsInCart.Pizzas,
                Sodas = itemsInCart.Sodas
            };

            _context.Add(order);
            await _context.SaveChangesAsync();

            return Ok($"Your order has been placed {order.Id}");
        }

        // PUT api/<OrderController>/5
        [HttpPut]
        [SwaggerOperation(Summary = "Update placed order")]
        public async Task<IActionResult> Put([FromBody] UpdateOrderStatusDTO request)
        {
            if (!ModelState.IsValid)
                BadRequest("Bad request");

            if (!request.Completed)
                Ok("No changes has been made.");

            var order = await _context.Orders.Where(x => x.IsActive && x.Id == request.OrderId).FirstOrDefaultAsync();

            if (order == null)
                return BadRequest("There's no active order with that Id.");

            order.IsActive = false;
            _context.Update(order);
            await _context.SaveChangesAsync();

            return Ok($"Order {order.Id} completed");
        }

        // DELETE api/<OrderController>/5
        [HttpDelete]
        [SwaggerOperation(Summary = "Cancel placed order")]
        public async Task<IActionResult> Delete([FromBody] CancelOrderDTO request)
        {
            if (!ModelState.IsValid)
                BadRequest("Bad request");

            if (!string.IsNullOrWhiteSpace(request.OrderIds.FirstOrDefault()))
                foreach (var orderId in request.OrderIds)
                {
                    var order = await _context.Orders.Where(x => x.IsActive && x.Id.ToString() == orderId)
                        .FirstOrDefaultAsync();

                    if (order != null)
                    {
                        order.IsActive = false;
                        _context.Update(order);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        return BadRequest("There's no active order with that Id.");
                    }
                }

            return Ok(new {canceledOrders = request.OrderIds});
        }
    }
}