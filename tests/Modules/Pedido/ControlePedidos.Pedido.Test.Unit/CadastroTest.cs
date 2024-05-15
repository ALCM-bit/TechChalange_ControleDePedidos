using ControlePedidos.Common.Entities;
using ControlePedidos.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlePedidos.Pedido.Test.Unit
{
    public class CadastroTest
    {

        private static Cadastro.Domain.Entities.Cadastro CriarCadastro()
        {
            string? id = Guid.NewGuid().ToString();
            string? nome = "Felipe";
            string? email = "felipe@gmail.com";
            string? cpf = "461.004.368-84";

            return new Cadastro.Domain.Entities.Cadastro(
                id,
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
                new Cadastro.Domain.ValueObjects.Email("felipe"),
                cadastro.CPF,
                cadastro.Nome
                );

            // Assert
            var notificationException = Assert.Throws<DomainException>(act);
        }

        [Fact]
        public void Validate_Should_ThrowDomainNotificationException_When_CPFForInvalido()
        {
            var cadastro = CriarCadastro();

            // Act
            Action act = () => new Cadastro.Domain.Entities.Cadastro(
                null,
                cadastro.Email,
                new Cadastro.Domain.ValueObjects.CPF("1254"),
                cadastro.Nome
                );

            // Assert
            var notificationException = Assert.Throws<DomainException>(act);
        }

        [Fact]
        public void Validate_Should_ThrowDomainNotificationException_When_NomeForVazio()
        {
            var cadastro = CriarCadastro();

            // Act
            Action act = () => new Cadastro.Domain.Entities.Cadastro(
                null,
                cadastro.Email,
                cadastro.CPF,
                ""
                );

            // Assert
            var notificationException = Assert.Throws<DomainException>(act);
        }

    }
}
