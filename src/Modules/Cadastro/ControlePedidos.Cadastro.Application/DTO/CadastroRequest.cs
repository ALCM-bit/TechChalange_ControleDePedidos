using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlePedidos.Cadastro.Application.DTO
{
    public class CadastroRequest
    {
        public string Email { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
    }
}
