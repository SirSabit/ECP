namespace Application.Dtos.OrderDtos
{
    public record OrderResponseDto(bool success,
    string message,
    PreorderDataDto data);
}
