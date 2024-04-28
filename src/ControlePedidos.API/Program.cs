using ControlePedidos.Pedido.Infrastructure.DependencyInjection;
using ControlePedidos.Cadastro.Infrastructure.DependencyInjection;
using ControlePedidos.Produto.Infrastructure.DependencyInjection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPedido(builder.Configuration);
builder.Services.AddCadastro(builder.Configuration);
builder.Services.AddProduto(builder.Configuration);

builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var options = new HealthCheckOptions();

options.ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse;

//app.UseHealthChecks(“/HealthCheck”, options);

app.UseHealthChecks("/healthz", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,

}).UseHealthChecksUI(h => h.UIPath = "/health-ui");

//app.UseEndpoints(endpoints => {

//    endpoints.MapHealthChecks("/healthz", options);
//});

app.MapHealthChecks("/healthz");

app.Run();
