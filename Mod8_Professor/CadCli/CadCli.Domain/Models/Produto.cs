using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadCli.Models
{
    [Table("Produto")]
    public class Produto:Entidade
    {

        [Column(TypeName = "varchar(100)"), Required]
        public string Nome { get; set; }

        [Column(TypeName = "money")]
        public decimal Preco { get; set; }

        public int TipoProdutoId { get; set; }
        [ForeignKey(nameof(TipoProdutoId))]
        public virtual TipoProduto TipoProduto { get; set; }

    }
}
