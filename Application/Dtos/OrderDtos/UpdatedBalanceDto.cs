namespace Application.Dtos.OrderDtos
{
    public record UpdatedBalanceDto(
    Guid userId,
    decimal totalBalance,
    decimal availableBalance,
    decimal blockedBalance,
    string currency,
    DateTime lastUpdated
);
}
