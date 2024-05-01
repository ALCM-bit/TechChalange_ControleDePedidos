using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlePedidos.Common.Entities
{
    public class RetornoPadrao
    {
        public IList<string> Mensagens { get; set; }
        public bool Status { get; set; }
    }
}