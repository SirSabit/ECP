using Dtos.InfrastructureDtos.HttpClientDtos.BalanceManagementClientDtos.OrderDtos;
using Dtos.InfrastructureDtos.HttpClientDtos.BalanceManagementClientDtos.ProductDtos;

namespace Infrastructure.HttpClients.Abstract
{
    public interface IBalanceManagementClient
    {
        Task<GetProductsClientResponseDto> GetProductsAsync();

        Task<OrderResponseDto> PreOrderAsync(PreOrderRequestDto request);

        Task<OrderResponseDto> CompleteAsync(CompleteOrderRequestDto request);
    }
}
