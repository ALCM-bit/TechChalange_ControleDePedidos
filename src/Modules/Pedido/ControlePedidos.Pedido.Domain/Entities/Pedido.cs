

namespace ControlePedidos.Pedido.Domain.Entities;

public class Pedido : Entity, IAggregationRoot
{
    public string Codigo { get; private set; }
    public string? IdCliente { get; private set; }
    public StatusPedido? Status { get; private set; } = null;

    // TODO: Adicionar Lista de Produtos

    public Pedido(string? id, string? codigoPedido, string? idCliente, StatusPedido? status, DateTime dataCriacao, DateTime? dataAtualizacao) : base(id, dataCriacao)
    {
        Codigo = !string.IsNullOrWhiteSpace(codigoPedido) ? codigoPedido : Guid.NewGuid().ToString().Substring(0, 5).ToUpper();
        IdCliente = idCliente;
        Status = status;
        DataAtualizacao = dataAtualizacao.HasValue ? dataAtualizacao.Value : null;

        Validate();
    }

    protected override void Validate()
    {
        if (DataAtualizacao.HasValue && (DataAtualizacao.Value < DataCriacao))
        {
            throw new DomainNotificationException("Data de finalização não pode ser menor que a data de criação");
        }
    }

    public double ObterTotal()
    {
        // TODO: Implementar quando modulo de produtos finalizado
        return 100;
    }

    public void ConfirmarPedido()
    {
        if (Status == StatusPedido.Recebido) return;

        if (Status != null)
        {
            throw new DomainNotificationException("Status inválido");
        }

        Status = StatusPedido.Recebido;

        Validate();
    }

    public void IniciarPreparo()
    {
        if (Status == StatusPedido.Preparando) return;

        if (Status != StatusPedido.Recebido)
        {
            throw new DomainNotificationException("Status inválido");
        }

        Status = StatusPedido.Preparando;

        Validate();
    }

    public void FinalizarPreparo()
    {
        if (Status == StatusPedido.Pronto) return;

        if (Status != StatusPedido.Preparando)
        {
            throw new DomainNotificationException("Status inválido");
        }

        Status = StatusPedido.Pronto;

        Validate();
    }

    public void FinalizarPedido()
    {
        if (Status == StatusPedido.Finalizado) return;

        if (Status != StatusPedido.Pronto)
        {
            throw new DomainNotificationException("Status inválido");
        }

        Status = StatusPedido.Finalizado;
        DataAtualizacao = DateTime.UtcNow;

        Validate();
    }

    public void AtualizarStatus(StatusPedido status)
    {
        switch (status)
        {
            case StatusPedido.Recebido:
                ConfirmarPedido();
                break;

            case StatusPedido.Preparando:
                IniciarPreparo();
                break;

            case StatusPedido.Pronto:
                FinalizarPreparo();
                break;

            case StatusPedido.Finalizado:
                FinalizarPedido();
                break;

            default:
                throw new DomainNotificationException("Status inválido");
        }

        Validate();
    }
}
