using Application.Dtos;
using Application.Dtos.OrderDtos;
using Application.Dtos.ProductDtos;
using Application.HttpClients.Abstract;
using System.Text;
using System.Text.Json;

namespace Application.HttpClients.Implementation
{
    public class BalanceManagementClient(HttpClient httpClient) : IBalanceManagementClient
    {
        public async Task<GetProductsClientResponseDto> GetProductsAsync()
        {
            string contentString = string.Empty;
            try
            {
                var httpRequest = await httpClient.GetAsync("/api/products");

                contentString = await httpRequest.Content.ReadAsStringAsync();
                
                httpRequest.EnsureSuccessStatusCode();

                var content = JsonSerializer.Deserialize<GetProductsClientResponseDto>(contentString);

                if (content is null)
                    throw new Exception("Content deserialization problem!");

                return content;

            }
            catch(Exception ex) 
            {

                var errorContent = JsonSerializer.Deserialize<BalanceManagementClientErrorResponseDto>(contentString);
                throw new Exception($"{errorContent?.error}\n{errorContent?.message}");
            }
        }

        public async Task<OrderResponseDto> PreOrderAsync(PreOrderRequestDto request)
        {
            string contentString = string.Empty;
            try
            {
                var json = JsonSerializer.Serialize(request);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                var httpRequest = await httpClient.PostAsync("/api/balance/preorder", httpContent);

                contentString = await httpRequest.Content.ReadAsStringAsync();

                httpRequest.EnsureSuccessStatusCode();

                var content = JsonSerializer.Deserialize<OrderResponseDto>(contentString);

                if (content is null)
                    throw new Exception("Content deserialization problem!");

                return content;
            }
            catch (Exception ex)
            {
                var errorContent = JsonSerializer.Deserialize<BalanceManagementClientErrorResponseDto>(contentString);
                throw new Exception($"{errorContent?.error}\n{errorContent?.message}");
            }
        }

        public async Task<OrderResponseDto> CompleteAsync(CompleteOrderRequestDto request)
        {
            string contentString = string.Empty;
            try
            {
                var json = JsonSerializer.Serialize(request);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                var httpRequest = await httpClient.PostAsync("/api/balance/complete", httpContent);

                contentString = await httpRequest.Content.ReadAsStringAsync();

                httpRequest.EnsureSuccessStatusCode();

                var content = JsonSerializer.Deserialize<OrderResponseDto>(contentString);

                if (content is null)
                    throw new Exception("Content deserialization problem!");

                return content;
            }
            catch (Exception ex)
            {
                var errorContent = JsonSerializer.Deserialize<BalanceManagementClientErrorResponseDto>(contentString);
                throw new Exception($"{errorContent?.error}\n{errorContent?.message}");
            }
        }
    }
}
