using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlePedidos.Cadastro.Infrastructure.Repositories.MongoDB.Models;

public class CadastroModel : BaseModel
{
        public CadastroModel(string cpf, string email, string nome)
        {
                Email = email;
                CPF = cpf;
                Nome = nome;
        }
        public string Email { get; private set; }
        public string CPF { get; private set; }
        public string Nome { get; private set; }
}
