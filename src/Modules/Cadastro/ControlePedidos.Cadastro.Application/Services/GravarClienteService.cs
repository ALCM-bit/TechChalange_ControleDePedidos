using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlePedidos.Cadastro.Domain.Entities;
using ControlePedidos.Cadastro.Infrastructure.Repositories.ClienteRepositories;
using ControlePedidos.Cadastro.Infrastructure.Repositories.MongoDB.Models;

namespace ControlePedidos.Cadastro.Application.Services
{
    public class GravarClienteService
    {
        private readonly ClienteRepository _clienteRepository;
        private readonly ValidarClienteService _validarClienteService;

        public GravarClienteService(
            ClienteRepository clienteRepository,
            ValidarClienteService validarClienteService)
        {
            _clienteRepository = clienteRepository;
            _validarClienteService = validarClienteService;
        }


        public async Task Cadastrar(Cliente cliente)
        {
            var validacaoOk = await _validarClienteService.ValidarCliente(cliente);

            if(!validacaoOk.Status)
            {
                return;
            }
            
            var model = new CadastroModel(cliente.Email.Endereco,cliente.CPF.Numero,cliente.Nome);

        }
    }
}