using ControlePedidos.Pedido.Infrastructure.DependencyInjection;
using ControlePedidos.Cadastro.Infrastructure.DependencyInjection;
using ControlePedidos.Produto.Infrastructure.DependencyInjection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
       .AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Modules
builder.Services.AddPedido(builder.Configuration);
builder.Services.AddCadastro(builder.Configuration);
builder.Services.AddProduto(builder.Configuration);

builder.Services.AddHealthChecks();
builder.Services.AddHealthChecks()
                .AddMongoDb(Environment.GetEnvironmentVariable("ConnectionStrings__ControlePedidosDB"));
builder.Services.AddHealthChecksUI(options =>
{
    options.SetEvaluationTimeInSeconds(5);
    options.MaximumHistoryEntriesPerEndpoint(10);
    options.AddHealthCheckEndpoint("Health Checks", "/health");
})
.AddInMemoryStorage();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHealthChecks("/health", new HealthCheckOptions
{
    Predicate = p => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.UseHealthChecksUI(options => options.UIPath = "/health-ui");

app.Run();
