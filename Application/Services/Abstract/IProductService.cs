using Application.Dtos.ProductDtos;

namespace Application.Services.Abstract
{
    public interface IProductService
    {
        /// <summary>
        /// Gets a list of products
        /// </summary>
        Task<List<ProductDto>> GetProducts();
    }
}
