using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CadCli.Models
{
    [Table(nameof(Produto))]
    public class Produto : Entidade
    {
        public int TipoProdutoId { get; set; }

        [Column(TypeName = "varchar(100)"), Required]
        public string Nome { get; set; }

        [Column(TypeName = "money")]
        public decimal Preco { get; set; }

        [ForeignKey(nameof(TipoProdutoId))]
        public virtual TipoProduto Tipo { get; set; }
    }
}
