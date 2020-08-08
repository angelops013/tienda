using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Tienda.Modelos.Entities
{
    public class Product
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public Decimal Value { get; set; }
        [JsonIgnore]
        public ICollection<Order> Orders { get; set; }
    }
}
