namespace ControlePedidos.Pedido.Domain.Factories;

public static class ItemPedidoFactory
{
    // TODO: Testes
    public static ItemPedido Criar(Produto produto, string? id, string tamanho, int quantidade, string? observacao)
    {
        if (produto is null || !produto.Ativo)
        {
            throw new DomainNotificationException("Produto inativo ou não encontrado");
        }

        KeyValuePair<string, decimal>? tamanhoPreco = produto.TamanhoPreco?.FirstOrDefault(x => x.Key?.ToLower() == tamanho?.ToLower());

        if (tamanhoPreco is null || tamanhoPreco.Value.Key is null)
        {
            throw new DomainNotificationException($"Tamanho [{tamanho}] não encontrado");
        }

        var itemPedido = new ItemPedido(id!,
                                        DateTime.UtcNow,
                                        produto.Id!,
                                        produto.Nome,
                                        produto.TipoProduto,
                                        tamanho,
                                        tamanhoPreco.Value.Value,
                                        quantidade,
                                        observacao);

        return itemPedido;
    }
}
