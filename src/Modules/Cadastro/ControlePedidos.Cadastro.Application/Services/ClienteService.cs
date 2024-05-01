using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlePedidos.Cadastro.Domain.Entities;
using ControlePedidos.Cadastro.Infrastructure.Repositories.MongoDB.Contexts;
using ControlePedidos.Cadastro.Infrastructure.Repositories.MongoDB.Models;
using MongoDB.Driver;

namespace ControlePedidos.Cadastro.Application.Services
{
    public class ClienteService : IClienteValidationService
    {
        private readonly IMongoCollection<CadastroModel> _dbCadastro;
        public ClienteService(CadastroDbContext dbContext)
        {
            _dbCadastro = dbContext.Cadastro;
        }

        public Cliente ObterService()
        {
            var context = _dbCadastro.Find(x => x.CPF == "00000000");
            return new Cliente();
        }
        public bool ValidarCPF(string cpf)
        {
            throw new NotImplementedException();
        }

        public bool ValidarEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
