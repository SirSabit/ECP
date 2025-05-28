using Application.Dtos.OrderDtos;
using Application.Services.Abstract;
using Asp.Versioning.Builder;

namespace Api.Endpoints.OrderEndpoints
{
    public static class OrderEndpoint
    {
        public static void MapOrderV1Endpoints(this WebApplication app, ApiVersionSet versionSet)
        {
            var group = app.MapGroup("/api/v{version:apiVersion}/orders")
               .WithApiVersionSet(versionSet)
               .MapToApiVersion(1);

            group.MapPost("create", async (PreOrderRequestDto request,IOrderService orderService) =>
            {
                var result = await orderService.CreateOrder(request);

                return Results.Ok(result);
            });

            group.MapPost("complete", async (CompleteOrderRequestDto request, IOrderService orderService) =>
            {
                var result = await orderService.CompleteOrder(request);

                return Results.Ok(result);
            });
        }
    }
}
