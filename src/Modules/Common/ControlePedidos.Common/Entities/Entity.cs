using Microsoft.VisualBasic;

namespace ControlePedidos.Common.Entities;

public abstract class Entity(string? id, DateTime dataCriacao)
{
    public string? Id { get; } = id;

    public DateTime DataCriacao { get; } = dataCriacao;

    public DateTime? DataAtualizacao { get; set; }

    protected abstract void Validate();
}
