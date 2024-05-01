using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlePedidos.Cadastro.Domain.Abstractions;
using ControlePedidos.Cadastro.Domain.Entities;
using ControlePedidos.Cadastro.Infrastructure.Repositories.ClienteRepositories;
using ControlePedidos.Cadastro.Infrastructure.Repositories.MongoDB.Models;

namespace ControlePedidos.Cadastro.Application.Services
{
    public class GravarClienteService
    {
        private readonly ICadastroRepository _cadastroRepository;
        private readonly ValidarClienteService _validarClienteService;

        public GravarClienteService(
            ICadastroRepository clienteRepository,
            ValidarClienteService validarClienteService)
        {
            _cadastroRepository = clienteRepository;
            _validarClienteService = validarClienteService;
        }


        public async Task Cadastrar(Domain.Entities.Cadastro cadastro)
        {
            var validacaoOk = await _validarClienteService.ValidarCliente(cadastro);

            if(!validacaoOk.Status)
            {
                return;
            }
            

        }
    }
}