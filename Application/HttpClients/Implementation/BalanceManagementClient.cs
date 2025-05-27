using Application.Dtos.ProductDtos;
using Application.HttpClients.Abstract;
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

                return content;

            }
            catch(Exception ex) 
            {

                var errorContent = JsonSerializer.Deserialize<GetProductsClientErrorResponse>(contentString);
                throw new Exception($"{errorContent.error}\n{errorContent.message}");
            }
        }
    }
}
