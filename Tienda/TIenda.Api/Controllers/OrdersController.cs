using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tienda.AccesoDatos;
using Tienda.Modelos.DTO;
using Tienda.Modelos.Entities;

namespace TIenda.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        readonly DataContext _dataContext;
        public OrdersController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ActionResult> Post([FromBody] CreateOrder createOrder)
        {
            Product product = await _dataContext.Products.FindAsync(createOrder.ProductId);
            var order = new Order
            {
                ProductId = product.Id,
                Value = product.Value,
                CustomerEmail = createOrder.CustomerEmail,
                CustomerName = createOrder.CustomerName,
                CustomerMobile = createOrder.CustomerMobile,
                Status = "CREATED",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            _dataContext.Orders.Add(order);
            await _dataContext.SaveChangesAsync();
            return Ok(new { order.Id });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get([FromRoute] Int32 id)
        {
            Order order = await _dataContext.Orders.FindAsync(id);
            _dataContext.Entry(order).Reference(b => b.Product).Load();
            return Ok(order);
        }
    }
}
