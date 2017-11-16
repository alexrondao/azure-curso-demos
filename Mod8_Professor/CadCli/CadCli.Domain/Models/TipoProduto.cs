using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CadCli.Models
{
    [Table(nameof(TipoProduto))]
    public class TipoProduto:Entidade
    {
        public string Nome { get; set; }
        public virtual ICollection<Produto> Produtos { get; set; }
    }
}