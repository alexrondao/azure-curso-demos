using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoAzureTableStorage
{
    public class Aluno: TableEntity
    {
        public Aluno()
        {

        }
        public Aluno(string nome, string sobrenome, int idade)
        {
            PartitionKey = sobrenome;
            RowKey = Guid.NewGuid().ToString();

            Nome = nome;
            Idade = idade;
        }

        public string Nome { get; set; }
        public int Idade { get; set; }
    }
}
