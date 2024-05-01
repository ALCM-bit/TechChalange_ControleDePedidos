using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlePedidos.Cadastro.Domain.Entities;
using ControlePedidos.Common.Entities;

namespace ControlePedidos.Cadastro.Application.Services
{
    public class ValidarClienteService
    {
        public async Task<RetornoPadrao> ValidarCliente(Cliente cliente)
        {

            var retorno = new RetornoPadrao();

            if(!string.IsNullOrWhiteSpace(cliente.CPF.Numero))
            {
                var cpfOk = cliente.CPF.Validate();
                if(!cpfOk)
                {
                    retorno.Mensagens.Add("O CPF informado é inválido.");
                }
            }

            if(!string.IsNullOrWhiteSpace(cliente.Email.Endereco))
            {
                var email = cliente.Email.Validate();
                if(!email)
                {
                    retorno.Mensagens.Add("O e-mail informado é inválido.");
                }
            }
            
            if(string.IsNullOrWhiteSpace(cliente.Nome))
            {
                retorno.Mensagens.Add("O nome é obrigatório.");
            }

            retorno.Status = retorno.Mensagens.Count() > 0 ? false : true;
            
            return retorno;
        }
    }
}