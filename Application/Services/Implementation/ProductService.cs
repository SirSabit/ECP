using Application.Services.Abstract;
using Dtos.InfrastructureDtos.HttpClientDtos.BalanceManagementClientDtos.ProductDtos;
using Infrastructure.HttpClients.Abstract;

namespace Application.Services.Implementation
{
    public class ProductService(IBalanceManagementClient balanceManagementClient) : IProductService
    {
        public async Task<List<ProductDto>> GetProducts()
        {
            var products = await balanceManagementClient.GetProductsAsync();

            return products.data;
        }
    }
}
