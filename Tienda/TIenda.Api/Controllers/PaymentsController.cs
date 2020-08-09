using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tienda.Funciones.Interfaces;

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
        [HttpPost("{orderId}")]
        public async Task<ActionResult> Post([FromRoute] Int32 orderId)
        {
            String userAgent = Request.Headers["User-Agent"].ToString();
            String remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            String urlRedirect = await _paymentFuntions.CreatePayment(orderId, userAgent, remoteIpAddress);
            if (String.IsNullOrEmpty(urlRedirect))
                return BadRequest();

            return Ok(new { urlRedirect });
        }
    }
}
