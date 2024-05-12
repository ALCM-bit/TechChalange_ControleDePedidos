using ControlePedidos.Cadastro.Application.Abstractions;
using ControlePedidos.Cadastro.Application.DTO;
using ControlePedidos.Cadastro.Application.Services;
using ControlePedidos.Cadastro.Domain.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlePedidos.Pedido.Test.Unit
{
    public class CadastroServiceTest
    {
        private readonly ICadastroService _cadastroService;
        private readonly Mock<ICadastroRepository> _cadastroRepository;

        public CadastroServiceTest()
        {
            _cadastroRepository = new Mock<ICadastroRepository>();
            _cadastroService = new CadastroService(_cadastroRepository.Object);
        }

        private static Cadastro.Domain.Entities.Cadastro CriarCadastro()
        {
            string? id = Guid.NewGuid().ToString();
            string? nome = "Felipe";
            string? email = "felipe@gmail.com";
            string? cpf = "46100436884";

            return new Cadastro.Domain.Entities.Cadastro(
                id, 
                new Cadastro.Domain.ValueObjects.Email(email), 
                new Cadastro.Domain.ValueObjects.CPF(cpf), 
                nome);
        }

        [Fact]
        public async Task ObterCadastroAsync_Shoul_RetornarCadastro_When_CadastroEncontrado()
        {
            var cadastro = CriarCadastro();

            _cadastroRepository.Setup(x => x.ObterCadastroAsync(cadastro.CPF.Numero)).ReturnsAsync(cadastro);

            var response = await _cadastroService.ObterCadastroAsync(cadastro.CPF.Numero);

            _cadastroRepository.Verify(x => x.ObterCadastroAsync(cadastro.CPF.Numero), Times.Once);
        }

        [Fact]
        public async Task ObterCadastroAsync_Shoul_RetornarNulo_When_CadastroNaoEncontrado()
        {
            var cpf = "11863931813";

            _cadastroRepository.Setup(x => x.ObterCadastroAsync(It.IsAny<string>())).ReturnsAsync(() => null!);

            var response = await _cadastroService.ObterCadastroAsync(cpf);

            _cadastroRepository.Verify(x => x.ObterCadastroAsync(cpf), Times.Once);

            Assert.Null(response);
        }

        [Fact]
        public async Task CriarCadastroAsync_Should_RetornarNome_When_CadastroCriado()
        {
            var request = new CadastroRequest()
            {
                CPF = "11863931813",
                Email = "felipe@gamil.com",
                Nome = "Felipe",
            };

            _cadastroRepository.Setup(x => x.CadastrarAsync(It.IsAny<Cadastro.Domain.Entities.Cadastro>()));

            var response = await _cadastroService.CadastrarAsync(request);

            _cadastroRepository.Verify(x => x.CadastrarAsync(It.IsAny<Cadastro.Domain.Entities.Cadastro>()), Times.Once);

            Assert.True(response);
        }
    }
}
