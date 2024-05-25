namespace ControlePedidos.Pedido.Domain.Entities;

public class Produto : Entity
{
    public string Nome { get; private set; }
    public IDictionary<TamanhoProduto, decimal> TamanhoPreco { get; init; }
    public string TipoProduto { get; private set; }
    public string Descricao { get; private set; }
    public bool Ativo { get; set; }

    public Produto(string id, string nome, IDictionary<TamanhoProduto, decimal> tamanhoPreco, string tipoProduto, string descricao, DateTime dataCriacao, bool ativo) : base(id, dataCriacao)
    {
        Nome = nome;
        TamanhoPreco = tamanhoPreco;
        TipoProduto = tipoProduto;
        Descricao = descricao;
        Ativo = ativo;

        Validate();
    }

    // TODO: Tests
    protected override void Validate()
    {
        if (string.IsNullOrWhiteSpace(Nome))
        {
            throw new DomainNotificationException("Nome do produto é obrigatório");
        }

        if (TamanhoPreco is null || !TamanhoPreco.Any())
        {
            throw new DomainNotificationException("Preço e tamanho do produto são obrigatórios");
        }

        if (string.IsNullOrWhiteSpace(TipoProduto))
        {
            throw new DomainNotificationException("Tipo do produto é obrigatório");
        }

        if (string.IsNullOrWhiteSpace(Descricao))
        {
            throw new DomainNotificationException("Descrição do produto é obrigatória");
        }
    }

    public void Criar()
    {
        Validate();
    }
}
