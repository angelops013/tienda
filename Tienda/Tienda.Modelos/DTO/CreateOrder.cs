using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tienda.Modelos.DTO
{
    public class CreateOrder
    {
        [Required]
        public String CustomerName { get; set; }
        [Required]
        public String CustomerEmail { get; set; }
        [Required]
        public String CustomerMobile { get; set; }
        [Required]
        public Int32 ProductId { get; set; }
    }
}
