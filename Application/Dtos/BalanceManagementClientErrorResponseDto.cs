using System.Text.Json.Serialization;

namespace Application.Dtos
{
    public record BalanceManagementClientErrorResponseDto(string error, string message);
   
}
