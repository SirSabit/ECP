using Application.Services.Abstract;
using Asp.Versioning.Builder;

namespace Api.Endpoints.ProductEndpoints
{
    public static class ProductEndpoint
    {
        /// <summary>
        /// Product endpoints V1
        /// </summary>
        /// <param name="versionSet">Version metadata of the endpoint</param>
        public static void MapProductV1Endpoints(this WebApplication app,ApiVersionSet versionSet)
        {
            var group = app.MapGroup("/api/v{version:apiVersion}/products")
                .WithApiVersionSet(versionSet)
                .MapToApiVersion(1);
            
            //Get the list of products
            group.MapGet("", async (IProductService productService) =>
            {
                var products = await productService.GetProducts();
                
                return Results.Ok(products);
            });
        }
    }
}
