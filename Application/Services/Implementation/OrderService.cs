using Application.Dtos.OrderDtos;
using Application.HttpClients.Abstract;
using Application.Services.Abstract;
using Domain.Exceptions;

namespace Application.Services.Implementation
{
    public class OrderService(IBalanceManagementClient balanceManagementClient) : IOrderService
    {
        public async Task<OrderResponseDto> CompleteOrder(CompleteOrderRequestDto request)
        {
            if (string.IsNullOrEmpty(request.orderId))
                throw new BadRequestException("You must enter a valid order id");

            var result = await balanceManagementClient.CompleteAsync(request);

            return result;

        }

        public async Task<OrderResponseDto> CreateOrder(PreOrderRequestDto request)
        {
            if (string.IsNullOrEmpty(request.orderId))
                request = request with { orderId = Guid.NewGuid().ToString() };

            if (request.amount <= 0)
                throw new BadRequestException("You must enter a valid amount");

            var result = await balanceManagementClient.PreOrderAsync(request);

            return result;
        }
    }
}
