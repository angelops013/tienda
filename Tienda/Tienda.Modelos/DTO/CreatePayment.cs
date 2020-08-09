using System;
using System.Collections.Generic;
using System.Text;

namespace Tienda.Modelos.DTO
{
    public class CreatePayment
    {
        public Int32 OrderId { get; set; }
        public String RemoteIpAddress { get; set; }
    }
}
