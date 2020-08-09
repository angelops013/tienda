using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tienda.AccesoDatos;
using Tienda.Funciones.Interfaces;
using Tienda.Modelos.Entities;

namespace Tienda.Funciones.Implementations
{
    public class ProductFuntions : IProductFuntions
    {
        readonly DataContext _dataContext;
        public ProductFuntions(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<List<Product>> GetAllProducts()
        {
            List<Product> products = await _dataContext.Products.ToListAsync();
            return products;
        }

        public async Task<Product> GetProduct(Int32 id)
        {
            Product product = await _dataContext.Products.FindAsync(id);
            return product;
        }
    }
}
