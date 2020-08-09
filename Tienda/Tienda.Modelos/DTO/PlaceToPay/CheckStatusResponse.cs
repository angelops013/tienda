using System;
using System.Collections.Generic;
using System.Text;

namespace Tienda.Modelos.DTO.PlaceToPay
{
    public class CheckStatusResponse
    {
        public Int32 requestId { get; set; }
        public Status status { get; set; }
        public Request request { get; set; }
        public List<PaymentP> payment { get; set; }

    }

    public class Request
    {
        public String locale { get; set; }
        public Payer payer { get; set; }
        public Buyer buyer { get; set; }
        public Payment payment { get; set; }
        public String returnUrl { get; set; }
        public String cancelUrl { get; set; }
        public String ipAddress { get; set; }
        public String userAgent { get; set; }
        public DateTime expiration { get; set; }
    }

    public class Payer
    {
        public String document { get; set; }
        public String documentType { get; set; }
        public String name { get; set; }
        public String surname { get; set; }
        public String email { get; set; }
        public String mobile { get; set; }
    }

    public class PaymentP
    {
        public Status status { get; set; }
        public Int32 internalReference { get; set; }
        public String paymentMethod { get; set; }
        public String paymentMethodName { get; set; }
        public String issuerName { get; set; }
        public AmountP amount { get; set; }
        public String authorization { get; set; }
        public String reference { get; set; }
        public String receipt { get; set; }
        public String franchise { get; set; }
        public Boolean refunded { get; set; }
        public Processorfield[] processorFields { get; set; }
    }

    public class AmountP
    {
        public From from { get; set; }
        public To to { get; set; }
        public Int32 factor { get; set; }
    }

    public class From
    {
        public String currency { get; set; }
        public Int32 total { get; set; }
    }

    public class To
    {
        public String currency { get; set; }
        public Int32 total { get; set; }
    }

    public class Processorfield
    {
        public String keyword { get; set; }
        public String value { get; set; }
        public String displayOn { get; set; }
    }
}
