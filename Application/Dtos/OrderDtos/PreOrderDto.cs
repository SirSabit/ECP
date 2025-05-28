namespace Application.Dtos.OrderDtos
{
    public record PreOrderDto(
    string orderId,
    decimal amount,
    DateTime timestamp,
    string status
);
}
