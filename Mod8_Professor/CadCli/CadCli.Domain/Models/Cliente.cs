﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadCli.Models
{
    [Table("Cliente")]
    public class Cliente : Entidade
    {

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Nome { get; set; }

        public int Idade { get; set; }
        public Sexo Sexo { get; set; }
    }


    public enum Sexo
    {
        Feminino, Masculino
    }
}
