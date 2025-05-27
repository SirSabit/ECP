using Microsoft.Extensions.DependencyInjection;
using Application.HttpClients.Abstract;
using Application.HttpClients.Implementation;
using Microsoft.Extensions.Configuration;
using Application.Services.Abstract;
using Application.Services.Implementation;
using Polly;


namespace Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplicationLayerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IBalanceManagementClient, BalanceManagementClient>(client =>
            {
                var baseUrl = configuration["HttpClients:BalanceManagement:Url"];
                var timeoutStr = configuration["HttpClients:BalanceManagement:TimeoutInSeconds"];

                if (string.IsNullOrWhiteSpace(baseUrl))
                    throw new InvalidOperationException("Product API BaseUrl is not configured.");

                if (!int.TryParse(timeoutStr, out var timeoutSeconds))
                    timeoutSeconds = 60; // default fallback

                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
            })
                .AddPolicyHandler(Policy<HttpResponseMessage>
                .Handle<HttpRequestException>()
                .OrResult(response => !response.IsSuccessStatusCode)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                onRetry: (outcome, timespan, retryCount, context) =>
                {
                    Console.WriteLine($"Retry {retryCount} after {timespan.TotalSeconds}s due to: {outcome.Exception?.Message ?? outcome.Result.StatusCode.ToString()}");
                }));

            services.AddScoped<IProductService, ProductService>();
        }
    }
}
