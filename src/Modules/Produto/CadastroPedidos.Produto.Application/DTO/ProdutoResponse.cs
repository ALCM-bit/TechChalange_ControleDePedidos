using ControlePedidos.Produto.Domain.Enums;

public class ProdutoResponse
{
    //  Colocar os campos do produto
    public string Id { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public TipoProduto TipoProduto { get; set; }
    public string Descricao { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime DataAtualizacao { get; set; }
    public bool Ativo { get; set; }
}
