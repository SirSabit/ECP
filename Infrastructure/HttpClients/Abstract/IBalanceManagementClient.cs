using Dtos.InfrastructureDtos.HttpClientDtos.BalanceManagementClientDtos.OrderDtos;
using Dtos.InfrastructureDtos.HttpClientDtos.BalanceManagementClientDtos.ProductDtos;

namespace Infrastructure.HttpClients.Abstract
{
    public interface IBalanceManagementClient
    {
        /// <summary>
        /// Get Products from Balance Client
        /// </summary>
        /// <returns></returns>
        Task<GetProductsClientResponseDto> GetProductsAsync();
        /// <summary>
        /// Create a pre order
        /// </summary>
        Task<OrderResponseDto> PreOrderAsync(PreOrderRequestDto request);
        /// <summary>
        /// Complete a pre order
        /// </summary>
        Task<OrderResponseDto> CompleteAsync(CompleteOrderRequestDto request);
    }
}
