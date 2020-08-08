using System;
using System.Collections.Generic;
using System.Text;

namespace Tienda.Modelos.Entities
{
    public class Payment
    {
        public Int32 Id { get; set; }
        public DateTime Date { get; set; }
        public Int32 RequestId { get; set; }
        public String MethodName { get; set; }
        public String Authorization { get; set; }
        public String IssuerName { get; set; }
        public String Receipt { get; set; }
        public String Status { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
