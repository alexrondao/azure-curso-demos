using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CadCli.Models
{
    [Table(nameof(TipoProduto))]
    public class TipoProduto : Entidade
    {
        public string Nome { get; set; }

        public virtual ICollection<Produto> Produtos { get; set; }
    }
}
