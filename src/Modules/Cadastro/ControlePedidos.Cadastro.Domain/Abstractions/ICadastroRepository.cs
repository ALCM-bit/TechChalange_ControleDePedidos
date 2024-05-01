using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ControlePedidos.Cadastro.Domain.Abstractions
{
    public interface ICadastroRepository
    {
        Task<Entities.Cadastro> ObterCadastro(string id);
        Task<IList<Entities.Cadastro>> ObterTodosCadastros();
    }
}