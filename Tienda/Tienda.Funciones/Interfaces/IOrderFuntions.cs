using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tienda.Modelos.DTO;
using Tienda.Modelos.Entities;

namespace Tienda.Funciones.Interfaces
{
    public interface IOrderFuntions
    {
        Task<Int32> CreateOrder(CreateOrder createOrder);
        Task<Order> GetOrder(Int32 id);
        Task<List<Order>> GetAllOrders();
    }
}
