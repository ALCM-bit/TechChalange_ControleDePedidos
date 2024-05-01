using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlePedidos.Cadastro.Domain.Abstractions;
using ControlePedidos.Cadastro.Domain.Entities;
using ControlePedidos.Cadastro.Domain.ValueObjects;
using ControlePedidos.Cadastro.Infrastructure.Repositories.ClienteRepositories;
using ControlePedidos.Cadastro.Infrastructure.Repositories.MongoDB.Contexts;
using ControlePedidos.Cadastro.Infrastructure.Repositories.MongoDB.Models;
using MongoDB.Driver;

namespace ControlePedidos.Cadastro.Application.Services
{
    public class ObterClienteService
    {
        private readonly ICadastroRepository _clienteRepository;

        public ObterClienteService(ICadastroRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<Domain.Entities.Cadastro> ObterCadastro(string Id)
        {
            var cadastro = await _clienteRepository.ObterCadastro(Id);
            return cadastro;
        }

        public async Task<IList<Domain.Entities.Cadastro>> ObterTodosCadastros()
        {
            var cadastros = await _clienteRepository.ObterTodosCadastros();
            return cadastros;
        }


    }
}
