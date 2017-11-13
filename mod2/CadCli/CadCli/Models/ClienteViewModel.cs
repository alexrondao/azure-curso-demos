using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CadCli.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Nome { get; set; }

        public int Idade { get; set; }
        public Sexo Sexo { get; set; }
    }

    public enum Sexo
    {
        Feminino,
        Masculino
    }
}
