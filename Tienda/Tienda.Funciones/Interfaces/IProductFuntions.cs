using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tienda.Modelos.Entities;

namespace Tienda.Funciones.Interfaces
{
    public interface IProductFuntions
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProduct(Int32 id);

    }
}
