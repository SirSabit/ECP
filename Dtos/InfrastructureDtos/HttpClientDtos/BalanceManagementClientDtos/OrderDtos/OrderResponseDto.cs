namespace Dtos.InfrastructureDtos.HttpClientDtos.BalanceManagementClientDtos.OrderDtos
{
    public record OrderResponseDto(bool success,
   string message,
   PreorderDataDto data);
}
