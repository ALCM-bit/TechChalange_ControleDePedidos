﻿using CadastroPedidos.Pedido.Application.DTO;
using ControlePedidos.Pedido.Domain.Enums;

namespace CadastroPedidos.Pedido.Application.Abstractions;

public interface IPedidoApplicationService
{
    Task<IEnumerable<PedidoResponse>> ObterTodosPedidosAsync();

    /// <summary>
    /// Cria um Pedido
    /// </summary>
    /// <param name="pedido"></param>
    /// <returns>Id do Pedido</returns>
    Task<string> CriarPedidoAsync(PedidoRequest pedido);
    Task AtualizarPedidoAsync(string id, AtualizarPedidoRequest pedidoRequest);
    Task<string> CheckoutPedido(string id);
}
