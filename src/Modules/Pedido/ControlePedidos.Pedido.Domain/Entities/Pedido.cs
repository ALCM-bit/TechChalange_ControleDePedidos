

namespace ControlePedidos.Pedido.Domain.Entities;

public class Pedido : Entity, IAggregationRoot
{
    public string Codigo { get; private set; }
    public string? IdCliente { get; private set; }
    public StatusPedido? Status { get; private set; } = null;
    public DateTime DataCriacao { get; private set; }
    public DateTime? DataFinalizacao { get; private set; }

    // TODO: Adicionar Lista de Produtos

    public Pedido(string id, string codigoPedido, DateTime dataCriacao, string? idCliente): base(id)
    {
        Codigo = codigoPedido;
        DataCriacao = dataCriacao;
        IdCliente = idCliente;

        Validate();
    }

    protected override void Validate()
    {
        if (DataFinalizacao.HasValue && (DataFinalizacao.Value < DataCriacao))
        {
            throw new DomainException("Data de finalização não pode ser menor que a data de criação");
        }
    }

    public void ConfirmarPedido()
    {
        if (Status != null)
        {
            throw new DomainException("Status inválido");
        }

        Status = StatusPedido.Recebido;
        Validate();
    }

    public void IniciarPreparo()
    {
        if (Status != StatusPedido.Recebido)
        {
            throw new DomainException("Status inválido");
        }

        Status = StatusPedido.Preparando;
        Validate();
    }

    public void FinalizarPreparo()
    {
        if (Status != StatusPedido.Preparando)
        {
            throw new DomainException("Status inválido");
        }

        Status = StatusPedido.Pronto;
        Validate();
    }

    public void FinalizarPedido()
    {
        if (Status != StatusPedido.Pronto)
        {
            throw new DomainException("Status inválido");
        }

        Status = StatusPedido.Finalizado;
        DataFinalizacao = DateTime.UtcNow;
        Validate();
    }

    public double ObterTotal()
    {
        // TODO: Implementar
        return 100;
    }
}
