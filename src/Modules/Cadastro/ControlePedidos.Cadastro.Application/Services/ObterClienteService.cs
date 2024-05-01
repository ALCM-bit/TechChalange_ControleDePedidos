using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private readonly ClienteRepository _clienteRepository;

        public ObterClienteService(ClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<Cliente> ObterCliente(string Id)
        {
            var cadastro = await _clienteRepository.ObterCliente(Id);
            
            var cliente = new Cliente( new Email(cadastro.Email), new CPF(cadastro.CPF), cadastro.Nome);
            

            return cliente;
        }

        public async Task<IList<Cliente>> ObterTodosClientes()
        {
            var cadastros = await _clienteRepository.ObterTodosClientes();

            var clientes = new List<Cliente>();
            
            foreach(var cadastro in cadastros)
            {
                clientes.Add(new Cliente(new Email(cadastro.Email), new CPF(cadastro.CPF), cadastro.Nome));
            }
            

            return clientes;
        }


    }
}
