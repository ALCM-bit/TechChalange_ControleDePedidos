using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlePedidos.Cadastro.Infrastructure.Repositories.MongoDB.Models;

namespace ControlePedidos.Cadastro.Domain.Abstractions
{
    public interface IClienteRepository
    {
        Task<CadastroModel> ObterCliente(string id);
        Task<IList<CadastroModel>> ObterTodosClientes();
    }
}