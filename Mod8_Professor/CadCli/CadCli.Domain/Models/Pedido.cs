using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadCli.Models
{
    public class Pedido:Entidade
    {
        public int ClienteId { get; set; }
        public int ProdutoId { get; set; }
        public bool Status { get; set; }
    }
}
