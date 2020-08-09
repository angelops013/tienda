using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Tienda.AccesoDatos;
using Tienda.Funciones.Interfaces;
using Tienda.Modelos.DTO.PlaceToPay;
using Tienda.Modelos.Entities;

namespace Tienda.Funciones.Implementations
{
    public class PaymentFuntions : IPaymentFuntions
    {
        readonly DataContext _dataContext;
        readonly IOrderFuntions _orderFuntions;
        readonly IConfiguration _configuration;
        public PaymentFuntions(DataContext dataContext, IOrderFuntions orderFuntions, IConfiguration configuration)
        {
            _dataContext = dataContext;
            _orderFuntions = orderFuntions;
            _configuration = configuration;
        }
        public async Task<String> CreatePayment(Int32 orderId, String userAgent, String remoteIpAddress)
        {
            var payment = new Modelos.Entities.Payment
            {
                Date = DateTime.Now,
                OrderId = orderId,
                Status = "Pendiente"
            };
            _dataContext.Payments.Add(payment);
            await _dataContext.SaveChangesAsync();
            _dataContext.Entry(payment).Reference(b => b.Order).Load();
            String urlRedirect = await InitiatePayment(payment, userAgent, remoteIpAddress);
            return urlRedirect;
        }

        async Task<String> InitiatePayment(Modelos.Entities.Payment paymentCreated, String userAgent, String remoteIpAddress)
        {
            String urlRedirect = String.Empty;
            PaymentRequest paymentRequest = CreateRequest(paymentCreated, userAgent, remoteIpAddress);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["Custom:P2PUrl"]);
                String jsonString = JsonConvert.SerializeObject(paymentRequest);
                StringContent stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await client.PostAsync("api/session", stringContent);
                String resultString = await responseMessage.Content.ReadAsStringAsync();

                var paymentResponse = JsonConvert.DeserializeObject<PaymentResponse>(resultString);

                if (paymentResponse.status.status.Equals("OK"))
                {
                    urlRedirect = paymentResponse.processUrl;
                    paymentCreated.RequestId = paymentResponse.requestId;
                }
                else
                {
                    paymentCreated.Status = "Rechazado";
                    urlRedirect = null;
                }

                _dataContext.Payments.Attach(paymentCreated);
                _dataContext.SaveChanges();
            }

            return urlRedirect;
        }

        PaymentRequest CreateRequest(Modelos.Entities.Payment paymentCreated, string userAgent, string remoteIpAddress)
        {
            var auth = new Auth()
            {
                login = _configuration["Custom:P2PLogin"],
                seed = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"),
                nonce = new Random().GetHashCode().ToString(),
                tranKey = _configuration["Custom:P2PTranKey"]
            };

            auth.tranKey = GetBase64(GetSha1Bytes($"{auth.nonce}{auth.seed}{auth.tranKey}"));
            auth.nonce = GetBase64(auth.nonce);

            var buyer = new Buyer()
            {
                name = paymentCreated.Order.CustomerName,
                email = paymentCreated.Order.CustomerEmail,
                mobile = paymentCreated.Order.CustomerMobile
            };

            var amount = new Amount()
            {
                currency = "COP",
                total = paymentCreated.Order.Value
            };

            Modelos.DTO.PlaceToPay.Payment payment = new Modelos.DTO.PlaceToPay.Payment()
            {
                reference = paymentCreated.Id.ToString(),
                description = "Prueba de pago",
                amount = amount
            };

            var PeticionPago = new PaymentRequest()
            {
                auth = auth,
                locale = "es_CO",
                buyer = buyer,
                payment = payment,
                expiration = DateTime.Now.AddHours(1).ToString("yyyy-MM-ddTHH:mm:sszzz"),
                returnUrl = $"{_configuration["Custom:UrlReturn"]}/payment/{paymentCreated.Id}",
                userAgent = userAgent,
                ipAddress = remoteIpAddress
            };
            return PeticionPago;
        }

        Byte[] GetSha1Bytes(String value)
        {
            Byte[] cripto;

            using (SHA1 hashString = new SHA1CryptoServiceProvider())
            {
                using MemoryStream stream = new MemoryStream();
                StreamWriter sw = new StreamWriter(stream);
                sw.Write(value);
                sw.Flush();
                stream.Position = 0;
                cripto = hashString.ComputeHash(stream);

            }

            return cripto;
        }

        String GetBase64(byte[] input)
        {
            return Convert.ToBase64String(input);
        }
        String GetBase64(String input)
        {
            if (input != null)
                return GetBase64(Encoding.UTF8.GetBytes(input));

            return "";
        }

        public async Task<Int32> CheckStatus(Int32 paymentId)
        {
            Modelos.Entities.Payment payment = await _dataContext.Payments.FindAsync(paymentId);
            _dataContext.Entry(payment).Reference(b => b.Order).Load();
            var auth = new Auth()
            {
                login = _configuration["Custom:P2PLogin"],
                seed = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"),
                nonce = new Random().GetHashCode().ToString(),
                tranKey = _configuration["Custom:P2PTranKey"]
            };

            auth.tranKey = GetBase64(GetSha1Bytes($"{auth.nonce}{auth.seed}{auth.tranKey}"));
            auth.nonce = GetBase64(auth.nonce);

            var checkStatus = new
            {
                auth
            };

            using var client = new HttpClient
            {
                BaseAddress = new Uri(_configuration["Custom:P2PUrl"])
            };
            String jsonString = JsonConvert.SerializeObject(checkStatus);
            StringContent stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync($"api/session/{payment.RequestId}", stringContent);
            String resultString = await responseMessage.Content.ReadAsStringAsync();

            var checkStatusResponse = JsonConvert.DeserializeObject<CheckStatusResponse>(resultString);

            if (checkStatusResponse.payment != null || checkStatusResponse.status.status == "REJECTED")
            {
                if (checkStatusResponse.payment.Count > 0)
                {
                    PaymentP paymentInfo = checkStatusResponse.payment.Any(p => p.status.status == "APPROVED") ?
                        checkStatusResponse.payment.FirstOrDefault(p => p.status.status == "APPROVED") : checkStatusResponse.payment.FirstOrDefault();
                    switch (paymentInfo.status.status)
                    {
                        case "APPROVED":
                            payment.IssuerName = paymentInfo.issuerName;
                            payment.MethodName = paymentInfo.paymentMethodName;
                            payment.Authorization = paymentInfo.authorization;
                            payment.Date = paymentInfo.status.date;
                            payment.Receipt = paymentInfo.receipt;
                            payment.Status = "Aprobado";
                            payment.Order.Status = "PAYED";
                            break;
                        case "REJECTED":
                        default:
                            payment.Status = "Rechazado";
                            payment.Order.Status = "REJECTED";
                            break;
                    }
                }
                else
                {
                    payment.Status = "Rechazado";
                    payment.Order.Status = "REJECTED";
                }
                _dataContext.Payments.Attach(payment);
                _dataContext.SaveChanges();
            }

            return payment.OrderId;
        }
    }
}
