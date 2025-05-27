using System.Text.Json.Serialization;

namespace Application.Dtos.ProductDtos
{
    public record GetProductsClientErrorResponse(string error, string message);
   
}
