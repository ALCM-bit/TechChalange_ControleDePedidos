using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using ControlePedidos.Cadastro.Domain.Abstractions;
using ControlePedidos.Cadastro.Domain.ValueObjects;
using ControlePedidos.Cadastro.Infrastructure.Repositories.MongoDB.Contexts;
using ControlePedidos.Cadastro.Infrastructure.Repositories.MongoDB.Models;
using Mapster;
using MongoDB.Driver;

namespace ControlePedidos.Cadastro.Infrastructure.Repositories.ClienteRepositories
{
    public class CadastroRepository : ICadastroRepository
    {
        private readonly CadastroDbContext _dbCadastro;
        public CadastroRepository(CadastroDbContext dbContext)
        {
            _dbCadastro = dbContext;
        }

        public async Task CadastrarAsync(Domain.Entities.Cadastro cadastro)
        {
            try
            {
                var cadastroModel = CadastroModel.MapFromDomain(cadastro);
                cadastroModel.CPF = cadastroModel.CPF.Trim().Replace(".", "").Replace("-", "");
                await _dbCadastro.Cadastro.InsertOneAsync(cadastroModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<Domain.Entities.Cadastro> ObterCadastroAsync(string cpf)
        {
            try
            {
                var cadastroEncontrado = await _dbCadastro.Cadastro.Find(x => x.CPF == cpf).FirstOrDefaultAsync();

                var cadastro = CadastroModel.MapToDomain(cadastroEncontrado);

                return cadastro;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<IList<Domain.Entities.Cadastro>> ObterTodosCadastrosAsync()
        {
            try
            {
                var cadastrosEncontrados = await _dbCadastro.Cadastro.Find(_ => true).ToListAsync();

                var cadastros = CadastroModel.MapToDomain(cadastrosEncontrados);

                return cadastros;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}