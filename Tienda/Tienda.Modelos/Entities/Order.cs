using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Tienda.Modelos.Entities
{
    public class Order
    {
        public Int32 Id { get; set; }
        public String CustomerName { get; set; }
        public String CustomerEmail { get; set; }
        public String CustomerMobile { get; set; }
        public String Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Decimal Value { get; set; }
        [JsonIgnore]
        public ICollection<Payment> Payments { get; set; }
        public Int32 ProductId { get; set; }
        public Product Product { get; set; }
    }
}
