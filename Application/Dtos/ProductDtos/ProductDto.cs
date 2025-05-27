
namespace Application.Dtos.ProductDtos
{
    public record ProductDto(string id, string name, string description, decimal price,string currency,string category,int stock);
}
