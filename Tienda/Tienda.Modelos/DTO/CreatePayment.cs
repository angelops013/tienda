using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tienda.Modelos.DTO
{
    public class CreatePayment
    {
        [Required]
        public Int32 OrderId { get; set; }
        [Required]
        public String RemoteIpAddress { get; set; }
    }
}
