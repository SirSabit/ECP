using Application.Dtos.ProductDtos;
using Application.HttpClients.Abstract;
using Application.Services.Abstract;

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
