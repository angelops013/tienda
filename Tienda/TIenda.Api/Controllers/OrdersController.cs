using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tienda.AccesoDatos;
using Tienda.Funciones.Interfaces;
using Tienda.Modelos.DTO;
using Tienda.Modelos.Entities;

namespace TIenda.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        readonly IOrderFuntions _orderFuntions;
        public OrdersController(IOrderFuntions orderFuntions)
        {
            _orderFuntions = orderFuntions;
        }

        public async Task<ActionResult> Post([FromBody] CreateOrder createOrder)
        {
            Int32 orderId = await _orderFuntions.CreateOrder(createOrder);
            return Ok(new { Id = orderId });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get([FromRoute] Int32 id)
        {
            Order order = await _orderFuntions.GetOrder(id);
            return Ok(order);
        }
    }
}
