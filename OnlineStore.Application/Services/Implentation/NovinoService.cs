using Newtonsoft.Json;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.DTOs;
using System.Text;

namespace OnlineStore.Application.Services.Implentation
{
    public class NovinoService(HttpClient httpClient) : INovinoService
    {
        public async Task<NovinoGetPaymentUrlResponseDto> CreateRequestAsync(NovinoGetPaymentUrlRequestDto model) 
        {
            string body = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(body, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://api.novinopay.com/payment/ipg/v2/request", content); 

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<NovinoGetPaymentUrlResponseDto>(responseString);

            return result;
        }

        public async Task<NovinoVerifyPaymentResponseDto> VerifyAsync(NovinoVerifyPaymentRequestDto model)
        {
            string body = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(body, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://api.novinopay.com/payment/ipg/v2/verification", content);

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<NovinoVerifyPaymentResponseDto>(responseString);

            return result;
        }
    }
}
