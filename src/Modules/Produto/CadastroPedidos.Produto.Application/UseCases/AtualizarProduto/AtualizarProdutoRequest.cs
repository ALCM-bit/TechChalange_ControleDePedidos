using ControlePedidos.Produto.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroPedidos.Produto.Application.UseCases.AtualizarProduto;

public class AtualizarProdutoRequest
{
    public string Id { get; set; }
    public required string Nome { get; set; }
    public required List<KeyValuePair<string, decimal>> TamanhoPreco { get; set; }
    public TipoProduto TipoProduto { get; set; }
    public required string Descricao { get; set; }
    public bool Ativo { get; set; }
}
