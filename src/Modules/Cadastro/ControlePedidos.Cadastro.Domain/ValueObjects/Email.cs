using ControlePedidos.Common.Entities;
using ControlePedidos.Common.Exceptions;

namespace ControlePedidos.Cadastro.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public Email(string email)
        {
            Endereco = email;

            if (!Validate())
            {
                throw new DomainException("O e-mail informado é inválido.");
            }


        }

        public string Endereco { get; private set; }

        public override bool Validate()
        {
            string email = Endereco;
            if (email.IndexOf("@") <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
