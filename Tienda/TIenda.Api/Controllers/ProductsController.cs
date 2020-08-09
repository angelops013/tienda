using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Tienda.AccesoDatos;
using Tienda.Funciones.Interfaces;
using Tienda.Modelos.Entities;

namespace TIenda.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly IProductFuntions _productFuntions;
        public ProductsController(IProductFuntions productFuntions)
        {
            _productFuntions = productFuntions;
        }

        public async Task<ActionResult> Get()
        {            
            List<Product> products = await _productFuntions.GetAllProducts();
            return Ok(products);
        }
    }
}
