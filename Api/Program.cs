using Api.Endpoints.OrderEndpoints;
using Api.Endpoints.ProductEndpoints;
using Api.Middlewares.ExceptionHandlers;
using Application.Extensions;
using Asp.Versioning;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

#region Exception Handlers
builder.Services.AddExceptionHandler<BadRequestExceptionHandler>();
builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
#endregion
builder.Services.AddProblemDetails();

builder.Services.AddApplicationLayerServices(builder.Configuration);

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

var apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1, 0))
    .HasApiVersion(new ApiVersion(1, 1))
    .ReportApiVersions()
    .Build();

app.MapProductV1Endpoints(apiVersionSet);
app.MapOrderV1Endpoints(apiVersionSet);

app.UseExceptionHandler();
app.UseHttpsRedirection();

app.Run();

