
using Dtos.InfrastructureDtos.HttpClientDtos.BalanceManagementClientDtos.OrderDtos;

namespace Application.Services.Abstract
{
    public interface IOrderService
    {
        Task<OrderResponseDto> CreateOrder(PreOrderRequestDto request);

        Task<OrderResponseDto> CompleteOrder(CompleteOrderRequestDto request);
    }
}
