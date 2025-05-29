using Application.Services.Abstract;
using Domain.Entities;
using Domain.Exceptions;
using Dtos.InfrastructureDtos.HttpClientDtos.BalanceManagementClientDtos.OrderDtos;
using Infrastructure.DbContexts.Abstract;
using Infrastructure.DbContexts.Implementation;
using Infrastructure.HttpClients.Abstract;
using System.Text.Json;

namespace Application.Services.Implementation
{
    public class OrderService(IBalanceManagementClient balanceManagementClient,IDbContext<PostgresDbContext> dbContext) : IOrderService
    {
        
        public async Task<OrderResponseDto> CompleteOrder(CompleteOrderRequestDto request)
        {
            if (string.IsNullOrEmpty(request.orderId))
                throw new BadRequestException("You must enter a valid order id");

            var result = await balanceManagementClient.CompleteAsync(request);
            
            await LogOrderResult(result);

            return result;

        }

        private async Task LogOrderResult(OrderResponseDto result)
        {
            await dbContext.Instance.OrderLogs.AddAsync(new OrderLogEntity
            {
                Message = result.message,
                Success = result.success,
                ResponseData = JsonSerializer.Serialize(result.data)
            });

            await dbContext.Instance.SaveChangesAsync();
        }

        public async Task<OrderResponseDto> CreateOrder(PreOrderRequestDto request)
        {
            if (string.IsNullOrEmpty(request.orderId))
                request = request with { orderId = Guid.NewGuid().ToString() };

            if (request.amount <= 0)
                throw new BadRequestException("You must enter a valid amount");

            var result = await balanceManagementClient.PreOrderAsync(request);

            await LogOrderResult(result);

            return result;
        }
    }
}
