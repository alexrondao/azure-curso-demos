using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadCli.Models
{
    public class Entidade
    {
        [Key]
        public int Id { get; set; }
        public DateTime DataCadastro { get; protected set; } = DateTime.Now;
    }
}
