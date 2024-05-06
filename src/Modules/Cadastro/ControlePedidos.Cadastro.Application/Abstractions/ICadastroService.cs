using ControlePedidos.Cadastro.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlePedidos.Cadastro.Application.Abstractions
{
    public interface ICadastroService
    {
        Task<bool> CadastrarAsync(CadastroRequest cadastro);
        Task<CadastroResponse> ObterCadastroAsync(string Id);
    }
}
