using Api.Endpoints.OrderEndpoints;
using Api.Endpoints.ProductEndpoints;
using Api.Middlewares.ExceptionHandlers;
using Application.Extensions;
using Asp.Versioning;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Exception Handlers
builder.Services.AddExceptionHandler<BadRequestExceptionHandler>();
builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
#endregion
builder.Services.AddProblemDetails();
builder.Services.AddApplicationLayerServices(builder.Configuration);

#region Api Versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

#endregion

var app = builder.Build();
//Api versioning parameters
var apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1, 0))
    .HasApiVersion(new ApiVersion(1, 1))
    .ReportApiVersions()
    .Build();


#region Api Version V1 EndpointMappings
app.MapProductV1Endpoints(apiVersionSet);
app.MapOrderV1Endpoints(apiVersionSet);
#endregion

app.UseExceptionHandler();
app.UseHttpsRedirection();

app.Run();

