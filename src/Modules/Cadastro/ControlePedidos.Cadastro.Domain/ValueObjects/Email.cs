using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ControlePedidos.Common.Entities;

namespace ControlePedidos.Cadastro.Domain.ValueObjects
{
    public class Email : ValueObjectBase
    {
        public Email(string email)
        {
            Endereco = email;
        }

        public string Endereco { get; private set; }

        public override bool Validate()
        {
            string email = Endereco;
            if(email.IndexOf("@") <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
