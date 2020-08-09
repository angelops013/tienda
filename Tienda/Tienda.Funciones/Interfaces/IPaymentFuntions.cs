using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.Funciones.Interfaces
{
    public interface IPaymentFuntions
    {
        Task<String> CreatePayment(Int32 orderId, String userAgent, String remoteIpAddress);
    }
}
