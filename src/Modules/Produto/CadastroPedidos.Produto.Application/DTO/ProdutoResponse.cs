using ControlePedidos.Produto.Domain.Enums;

public class ProdutoResponse
{
    public string Id { get; set; }
    public string Nome { get; set; }
    public List<KeyValuePair<string, decimal>> TamanhoPreco { get; set; }
    public TipoProduto TipoProduto { get; set; }
    public string Descricao { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime DataAtualizacao { get; set; }
    public bool Ativo { get; set; }
}
