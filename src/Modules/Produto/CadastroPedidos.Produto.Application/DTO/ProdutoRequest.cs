using ControlePedidos.Produto.Domain.Enums;

public class ProdutoRequest
{
    //  Colocar os campos do produto
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public TipoProduto TipoProduto { get; set; }
    public string Descricao { get; set; }
}
