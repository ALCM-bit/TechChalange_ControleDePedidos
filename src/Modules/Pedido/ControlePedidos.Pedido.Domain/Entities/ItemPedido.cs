namespace ControlePedidos.Pedido.Domain.Entities;

public class ItemPedido : Entity
{
    public string ProdutoId { get; }
    public string Nome { get; }
    public string TipoProduto { get; }
    public TamanhoProduto Tamanho { get; private set; }
    public decimal Preco { get; private set; }
    public int Quantidade { get; private set; }
    public string? Observacao { get; private set; }
    public decimal Subtotal => Preco * Quantidade;

    public ItemPedido(string? id,
                   DateTime dataCriacao,
                   string produtoId,
                   string nome,
                   string tipoProduto,
                   TamanhoProduto tamanho,
                   decimal preco,
                   int quantidade,
                   string? observacao) : base(id, dataCriacao)
    {
        ProdutoId = produtoId;
        Nome = nome;
        TipoProduto = tipoProduto;
        Tamanho = tamanho;
        Preco = preco;        
        Quantidade = quantidade;
        Observacao = observacao;

        Validate();
    }

    protected override void Validate()
    {
        if (Quantidade <= 0)
        {
            throw new DomainNotificationException("Quantidade precisar ser maior que 0");
        }
    }
}
