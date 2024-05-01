using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ControlePedidos.Cadastro.Domain.ValueObjects
{
    public class Email
    {
        public Email(string endereco)
        {
            Endereco = endereco;
        }

        public string Endereco { get; private set; }

        public bool Validar(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9.!#$%&'+/=?^_`{|}~-]+@a-zA-Z0-9?(?:.a-zA-Z0-9?)$";
            return Regex.IsMatch(email, emailPattern, RegexOptions.IgnoreCase);
        }
    }
}
