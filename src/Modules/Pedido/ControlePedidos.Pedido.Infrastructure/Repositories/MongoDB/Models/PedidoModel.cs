

using ControlePedidos.Pedido.Domain.Enums;

namespace ControlePedidos.Pedido.Infrastructure.Repositories.MongoDB.Models;

internal class PedidoModel : BaseModel
{
    public string Codigo { get; set; } = string.Empty;
    public string? IdCliente { get; set; }
    public StatusPedido? Status { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataFinalizacao { get; set; }
}
