namespace CadastroPedidos.Pedido.Application.UseCases.ProcessarPagamento;

public class ProcessarPagamentoPedidoRequest
{
    public string IdPedido { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}
