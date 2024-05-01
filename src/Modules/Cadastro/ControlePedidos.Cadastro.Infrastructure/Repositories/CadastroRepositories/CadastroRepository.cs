using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using ControlePedidos.Cadastro.Domain.Abstractions;
using ControlePedidos.Cadastro.Domain.ValueObjects;
using ControlePedidos.Cadastro.Infrastructure.Repositories.MongoDB.Contexts;
using ControlePedidos.Cadastro.Infrastructure.Repositories.MongoDB.Models;
using MongoDB.Driver;

namespace ControlePedidos.Cadastro.Infrastructure.Repositories.ClienteRepositories
{
    public class CadastroRepository: ICadastroRepository
    {
        private readonly IMongoCollection<CadastroModel> _dbCadastro;
        public CadastroRepository(CadastroDbContext dbContext)
        {
            _dbCadastro = dbContext.Cadastro;
        }

        public async Task<Domain.Entities.Cadastro> ObterCadastro(string id)
        {
            try
            {
                var cadastroEncontrado = await _dbCadastro.Find(x => x.Id == id).FirstOrDefaultAsync();

                var cadastro = new Domain.Entities.Cadastro(new Email(cadastroEncontrado.Email), new CPF(cadastroEncontrado.CPF), cadastroEncontrado.Nome);

                return cadastro;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

         public async Task<IList<Domain.Entities.Cadastro>> ObterTodosCadastros()
        {
            try
            {
                var cadastrosEncontrados = await _dbCadastro.Find(_ => true).ToListAsync();

                var cadastros = cadastrosEncontrados?.Select(x => new Domain.Entities.Cadastro(new Email(x.Email), new CPF(x.CPF), x.Nome)).ToList();

                return cadastros;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}