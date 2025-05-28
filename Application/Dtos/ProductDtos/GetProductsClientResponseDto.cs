namespace Application.Dtos.ProductDtos
{
    public record GetProductsClientResponseDto(bool success, List<ProductDto> data);
}
