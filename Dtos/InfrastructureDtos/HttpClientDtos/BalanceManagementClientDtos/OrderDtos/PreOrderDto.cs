namespace Dtos.InfrastructureDtos.HttpClientDtos.BalanceManagementClientDtos.OrderDtos
{
    public record PreOrderDto(
        string orderId,
        decimal amount,
        DateTime timestamp,
        string status
        );
}
