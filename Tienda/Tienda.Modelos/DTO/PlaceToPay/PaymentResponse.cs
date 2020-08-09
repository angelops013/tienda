using System;
using System.Collections.Generic;
using System.Text;

namespace Tienda.Modelos.DTO.PlaceToPay
{
    public class PaymentResponse
    {
        public Status status { get; set; }
        public Int32 requestId { get; set; }
        public String processUrl { get; set; }
    }

    public class Status
    {
        public String status { get; set; }
        public String reason { get; set; }
        public String message { get; set; }
        public DateTime date { get; set; }
    }
}
