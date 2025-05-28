namespace Application.Dtos.ProductDtos
{
    public record GetProductsClientResponse(bool success, List<ProductDto> data);
}
