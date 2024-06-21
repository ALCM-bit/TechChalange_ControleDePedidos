namespace CadastroPedidos.Pedido.Application.DTO;

public class ItemPedidoResponseDto
{
    //public string? Id { get; set; }
    public string ProdutoId { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string TipoProduto { get; set; } = string.Empty;
    public string Tamanho { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public int Quantidade { get; set; }
    public string? Observacao { get; set; }
    public decimal Subtotal { get; set; }
}
