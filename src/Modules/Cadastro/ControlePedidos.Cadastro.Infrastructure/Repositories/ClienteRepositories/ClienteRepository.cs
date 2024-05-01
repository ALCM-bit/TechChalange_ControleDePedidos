using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlePedidos.Cadastro.Domain.Abstractions;
using ControlePedidos.Cadastro.Infrastructure.Repositories.MongoDB.Contexts;
using ControlePedidos.Cadastro.Infrastructure.Repositories.MongoDB.Models;
using MongoDB.Driver;

namespace ControlePedidos.Cadastro.Infrastructure.Repositories.ClienteRepositories
{
    public class ClienteRepository: IClienteRepository
    {
        private readonly IMongoCollection<CadastroModel> _dbCadastro;
        public ClienteRepository(CadastroDbContext dbContext)
        {
            _dbCadastro = dbContext.Cadastro;
        }

        public async Task<CadastroModel> ObterCliente(string id)
        {
            try
            {
                var cadastro = await _dbCadastro.Find(x => x.Id == id).FirstOrDefaultAsync();

                return cadastro;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

         public async Task<IList<CadastroModel>> ObterTodosClientes()
        {
            try
            {
                var cadastros = await _dbCadastro.Find(_ => true).ToListAsync();

                return cadastros;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}