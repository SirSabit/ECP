namespace Dtos.InfrastructureDtos.HttpClientDtos.BalanceManagementClientDtos.ProductDtos
{
    public record GetProductsClientResponseDto(bool success, List<ProductDto> data);
}
