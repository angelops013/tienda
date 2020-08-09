using System;
using System.Collections.Generic;
using System.Text;

namespace Tienda.Modelos.DTO.PlaceToPay
{
    public class PaymentRequest
    {
        public Auth auth { get; set; }
        public String locale { get; set; }
        public Buyer buyer { get; set; }
        public Payment payment { get; set; }
        public String expiration { get; set; }
        public String returnUrl { get; set; }
        public String userAgent { get; set; }
        public String ipAddress { get; set; }
    }

    public class Auth
    {
        public String login { get; set; }
        public String seed { get; set; }
        public String nonce { get; set; }
        public String tranKey { get; set; }
    }

    public class Buyer
    {
        public String name { get; set; }
        public String email { get; set; }
        public String mobile { get; set; }
    }

    public class Payment
    {
        public String reference { get; set; }
        public String description { get; set; }
        public Amount amount { get; set; }
    }

    public class Amount
    {
        public String currency { get; set; }
        public Decimal total { get; set; }
    }
}
