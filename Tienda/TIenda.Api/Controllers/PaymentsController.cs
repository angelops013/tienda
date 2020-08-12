using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tienda.Funciones.Interfaces;
using Tienda.Modelos.DTO;

namespace TIenda.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        readonly IPaymentFuntions _paymentFuntions;
        public PaymentsController(IPaymentFuntions paymentFuntions)
        {
            _paymentFuntions = paymentFuntions;
        }
        public async Task<ActionResult> Post([FromBody] CreatePayment createPayment)
        {
            if (createPayment.OrderId <= 0)
                return BadRequest();
            String userAgent = Request.Headers["User-Agent"].ToString();
            String urlRedirect = await _paymentFuntions.CreatePayment(createPayment.OrderId, userAgent, createPayment.RemoteIpAddress);
            if (String.IsNullOrEmpty(urlRedirect))
                return BadRequest();

            return Ok(new { urlRedirect });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get([FromRoute] Int32 id)
        {
            Int32 orderId = await _paymentFuntions.CheckStatus(id);
            return Ok(new { orderId });
        }
    }
}
