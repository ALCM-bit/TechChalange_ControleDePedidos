using ControlePedidos.Produto.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroPedidos.Produto.Application.UseCases.ObterTodosProdutos;

public class ObterTodosProdutosRequest
{
    public TipoProduto? TipoProduto { get; set; }
    public bool Ativo { get; set; }
    public bool RetornarTodos { get; set; } = false;
}
