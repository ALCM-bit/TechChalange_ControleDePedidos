namespace CadastroPedidos.Pedido.Application.DTO;

public class ItemPedidoRequestDto
{
    public string ProdutoId { get; set; } = string.Empty;
    public string Tamanho { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public string? Observacao { get; set; }
}
