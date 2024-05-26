using ControlePedidos.Pedido.Domain.Enums;
using System.Text.Json.Serialization;

namespace CadastroPedidos.Pedido.Application.DTO;

public class PagamentoResponse
{
    public string Url { get; set; } = string.Empty;
}