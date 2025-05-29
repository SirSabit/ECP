
using Dtos.InfrastructureDtos.HttpClientDtos.BalanceManagementClientDtos.OrderDtos;

namespace Application.Services.Abstract
{
    public interface IOrderService
    {
        /// <summary>
        /// Create a new order using Balance Client
        /// </summary>
        /// <param name="request">Contains orderId and amount for new order</param>
        Task<OrderResponseDto> CreateOrder(PreOrderRequestDto request);

        /// <summary>
        /// Completes an order that is given with orderId
        /// </summary>
        /// <param name="request">Takes order id</param>
        Task<OrderResponseDto> CompleteOrder(CompleteOrderRequestDto request);
    }
}
