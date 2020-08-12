using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tienda.AccesoDatos;
using Tienda.Funciones.Interfaces;
using Tienda.Modelos.DTO;
using Tienda.Modelos.Entities;

namespace Tienda.Funciones.Implementations
{
    public class OrderFuntions : IOrderFuntions
    {
        readonly DataContext _dataContext;
        readonly IProductFuntions _productFuntions;
        public OrderFuntions(DataContext dataContext, IProductFuntions productFuntions)
        {
            _dataContext = dataContext;
            _productFuntions = productFuntions;
        }
        public async Task<Int32> CreateOrder(CreateOrder createOrder)
        {
            Product product = await _productFuntions.GetProduct(createOrder.ProductId);
            if (product is null)
                return -1;

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

            return order.Id;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            List<Order> orders = await _dataContext.Orders.Include(o => o.Product).ToListAsync();
            return orders;
        }

        public async Task<Order> GetOrder(Int32 id)
        {
            if (await ExistOrder(id))
            {
                Order order = await _dataContext.Orders.FindAsync(id);
                _dataContext.Entry(order).Reference(b => b.Product).Load();
                return order;
            }
            return null;
        }

        public async Task<Boolean> ExistOrder(Int32 id)
        {
            return await _dataContext.Orders.AnyAsync(o => o.Id == id);
        }
    }
}
