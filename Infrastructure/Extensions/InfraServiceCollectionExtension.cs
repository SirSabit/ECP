using Infrastructure.HttpClients.Abstract;
using Infrastructure.HttpClients.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace Infrastructure.Extensions
{
    public static class InfraServiceCollectionExtension
    {
        public static void AddInfrastructureLayerServices(this IServiceCollection services, IConfiguration configuration)
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
        }
    }
}
