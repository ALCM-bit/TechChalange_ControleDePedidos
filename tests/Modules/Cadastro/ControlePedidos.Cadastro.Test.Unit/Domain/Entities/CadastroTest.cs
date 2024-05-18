using ControlePedidos.Cadastro.Domain.Entities;
using ControlePedidos.Common.Exceptions;
namespace ControlePedidos.Pedido.Test.Unit
{
    public class CadastroTest
    {

        private static Cadastro.Domain.Entities.Cadastro CriarCadastro()
        {
            string id = Guid.NewGuid().ToString();
            string nome = "Felipe";
            DateTime dataDeCriacao = DateTime.Now;
            string email = "felipe@gmail.com";
            string cpf = "179.938.500-02";

            return new Cadastro.Domain.Entities.Cadastro(
                id,
                dataDeCriacao,
                new Cadastro.Domain.ValueObjects.Email(email),
                new Cadastro.Domain.ValueObjects.CPF(cpf),
                nome);
        }

        [Fact]
        public void Validate_Should_ThrowDomainNotificationException_When_EmailForInvalido()
        {
            var cadastro = CriarCadastro();

            // Act
            Action act = () => new Cadastro.Domain.Entities.Cadastro(
                null,
                DateTime.UtcNow,
                new Cadastro.Domain.ValueObjects.Email("felipe"),
                cadastro.CPF,
                cadastro.Nome
                );

            // Assert
            var notificationException = Assert.Throws<DomainNotificationException>(act);
        }

        [Fact]
        public void Validate_Should_ThrowDomainNotificationException_When_CPFForInvalido()
        {
            var cadastro = CriarCadastro();

            // Act
            Action act = () => new Cadastro.Domain.Entities.Cadastro(
                null,
                DateTime.UtcNow,
                cadastro.Email,
                new Cadastro.Domain.ValueObjects.CPF("1254"),
                cadastro.Nome
                );

            // Assert
            var notificationException = Assert.Throws<DomainNotificationException>(act);
        }

        [Fact]
        public void Validate_Should_ThrowDomainNotificationException_When_NomeForVazio()
        {
            var cadastro = CriarCadastro();

            // Act
            Action act = () => new Cadastro.Domain.Entities.Cadastro(
                null,
                DateTime.UtcNow,
                cadastro.Email,
                cadastro.CPF,
                ""
                );

            // Assert
            var notificationException = Assert.Throws<DomainNotificationException>(act);
        }

        [Fact]
        public void Validate_Shouldnt_ThrowDomainNotificationException_When_CadastroEstiverCorreto()
        {
            var cadastro = CriarCadastro();

            // Act
            Action act = () => new Cadastro.Domain.Entities.Cadastro(
                null,
                DateTime.UtcNow,
                cadastro.Email,
                cadastro.CPF,
                cadastro.Nome
                );

            // Assert
            Assert.NotNull(act);
        }

    }
}
