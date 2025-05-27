using Application.Dtos.ProductDtos;

namespace Application.HttpClients.Abstract
{
    public interface IBalanceManagementClient
    {
        Task<GetProductsClientResponseDto> GetProductsAsync();
    }
}
