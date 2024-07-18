using Microsoft.Extensions.Configuration;
using OMS.Core.Entities.Orders;
using OMS.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OMS.Servise
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public PaymentService(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<PaymentResult> ProcessPayment(Order order)
        {
            switch (order.PaymentMethod)
            {
                case PaymentMethod.CreditCard:
                    return await ProcessCreditCardPayment(order);
                case PaymentMethod.PayPal:
                    return await ProcessPayPalPayment(order);
                case PaymentMethod.BankTransfer:
                    return await ProcessBankTransferPayment(order);
                default:
                    throw new ArgumentException("Invalid payment method");
            }
        }

        private async Task<PaymentResult> ProcessCreditCardPayment(Order order)
        {
            var paymentGatewayUrl = _configuration["PaymentGateway:Url"];
            var apiKey = _configuration["PaymentGateway:ApiKey"];

            var paymentRequest = new
            {
                CardNumber = "1234567890123456", // Replace with order.CardNumber
                ExpiryDate = "12/25", // Replace with order.ExpiryDate
                Cvv = "123", // Replace with order.Cvv
                Amount = order.TotaL
            };

            var response = await _httpClient.PostAsJsonAsync($"{paymentGatewayUrl}/process", paymentRequest);
            if (!response.IsSuccessStatusCode)
            {
                return new PaymentResult
                {
                    Success = false,
                    Message = "Credit card payment failed"
                };
            }

            var result = await response.Content.ReadFromJsonAsync<PaymentResult>();
            return result;
        }

        private async Task<PaymentResult> ProcessPayPalPayment(Order order)
        {
            var payPalUrl = _configuration["PayPal:Url"];
            var clientId = _configuration["PayPal:ClientId"];
            var clientSecret = _configuration["PayPal:ClientSecret"];

            var authResponse = await _httpClient.PostAsync($"{payPalUrl}/v1/oauth2/token",
                new StringContent($"grant_type=client_credentials&client_id={clientId}&client_secret={clientSecret}"));

            if (!authResponse.IsSuccessStatusCode)
            {
                return new PaymentResult
                {
                    Success = false,
                    Message = "Failed to authenticate with PayPal"
                };
            }

            var authResult = await authResponse.Content.ReadFromJsonAsync<AuthResult>();
            var accessToken = authResult.AccessToken;

            var paymentRequest = new
            {
                intent = "sale",
                payer = new { payment_method = "paypal" },
                transactions = new[]
                {
                    new
                    {
                        amount = new { total = order.TotaL.ToString("F2"), currency = "USD" },
                        description = "Order payment"
                    }
                },
                redirect_urls = new
                {
                    return_url = "https://example.com/return",
                    cancel_url = "https://example.com/cancel"
                }
            };

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{payPalUrl}/v1/payments/payment")
            {
                Content = JsonContent.Create(paymentRequest)
            };
            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var paymentResponse = await _httpClient.SendAsync(requestMessage);
            if (!paymentResponse.IsSuccessStatusCode)
            {
                return new PaymentResult
                {
                    Success = false,
                    Message = "PayPal payment failed"
                };
            }

            var paymentResult = await paymentResponse.Content.ReadFromJsonAsync<PaymentResult>();
            return paymentResult;
        }

        private async Task<PaymentResult> ProcessBankTransferPayment(Order order)
        {
            var bankTransferInstructions = new
            {
                BankName = "Bank of Example",
                AccountNumber = "123456789",
                AccountName = "Example Store",
                Amount = order.TotaL,
                Reference = $"Order-{order.Id}"
            };

            Console.WriteLine($"Bank transfer instructions: {JsonSerializer.Serialize(bankTransferInstructions)}");

            return await Task.FromResult(new PaymentResult
            {
                Success = true,
                Message = "Bank transfer instructions generated",
                TransactionId = $"BT-{Guid.NewGuid()}",
                PaymentDate = DateTime.UtcNow
            });
        }
    }
    public class AuthResult
    {
        public string AccessToken { get; set; }
    }
}