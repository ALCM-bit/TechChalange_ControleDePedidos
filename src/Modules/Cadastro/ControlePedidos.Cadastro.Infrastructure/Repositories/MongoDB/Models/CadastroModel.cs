using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlePedidos.Cadastro.Infrastructure.Repositories.MongoDB.Models;

public class CadastroModel : BaseModel
{
        public string Email { get; set; }
        public string CPF { get; private set; }
        public string Nome { get; set; }
}
