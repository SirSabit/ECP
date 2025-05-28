using Application.Dtos.OrderDtos;

namespace Application.Services.Abstract
{
    public interface IOrderService
    {
        Task<OrderResponseDto> CreateOrder(PreOrderRequestDto request);

        Task<OrderResponseDto> CompleteOrder(CompleteOrderRequestDto request);
    }
}
