using Application.Services.Abstract;
using Domain.Exceptions;
using Dtos.InfrastructureDtos.HttpClientDtos.BalanceManagementClientDtos.ProductDtos;
using Infrastructure.HttpClients.Abstract;

namespace Application.Services.Implementation
{
    public class ProductService(IBalanceManagementClient balanceManagementClient) : IProductService
    {
        public async Task<List<ProductDto>> GetProducts()
        {
            var products = await balanceManagementClient.GetProductsAsync();

            if(products is null || products.data.Count == 0)
                throw new NotFoundException("No data was found");

            return products.data;
        }
    }
}
