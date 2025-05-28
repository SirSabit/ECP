using Application.Dtos.OrderDtos;
using Application.Dtos.ProductDtos;

namespace Application.HttpClients.Abstract
{
    public interface IBalanceManagementClient
    {
        Task<GetProductsClientResponse> GetProductsAsync();

        Task<OrderResponseDto> PreOrderAsync(PreOrderRequestDto request);

        Task<OrderResponseDto> CompleteAsync(CompleteOrderRequestDto request);
    }
}
