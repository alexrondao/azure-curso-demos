using CadCli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadCli.Data
{
    public class DbInitializer
    {

        public static void Initialize(CadCliDataContext context)
        {

            context.Database.EnsureCreated();

            if (!context.Clientes.Any())
            {
                context.Clientes.AddRange(

                    new List<Cliente>() {

                    new Cliente() { Nome = "Fabiano Nalin", Idade = 38, Sexo = Sexo.Masculino },
                    new Cliente() { Nome = "Priscila Mitui", Idade = 39, Sexo = Sexo.Feminino },
                    new Cliente() { Nome = "Raphael Nalin", Idade = 18, Sexo = Sexo.Masculino },
                    new Cliente() { Nome = "Raimundo Nonato", Idade = 78, Sexo = Sexo.Masculino },
                    new Cliente() { Nome = "José da Silva", Idade = 58, Sexo = Sexo.Masculino },
                    new Cliente() { Nome = "Paula dos Santos", Idade = 28, Sexo = Sexo.Feminino},
                    new Cliente() { Nome = "Daniel Augusto", Idade = 40, Sexo = Sexo.Masculino },
                    new Cliente() { Nome = "Carlos Adriano", Idade = 35, Sexo = Sexo.Masculino },
                    new Cliente() { Nome = "Fernanda Franco", Idade = 29, Sexo = Sexo.Feminino},
                    new Cliente() { Nome = "Isabel Aparecida", Idade = 58, Sexo = Sexo.Feminino},
                    }

                    );

                var vestuario = new TipoProduto() {Nome="Vestuário" };
                var higiene = new TipoProduto() {Nome="Higiene" };
                var papelaria = new TipoProduto() {Nome= "Papelaria" };

                if (!context.Produtos.Any())
                {
                    context.Produtos.AddRange(
                        
                        new Produto[] {
                            new Produto(){Nome="Camisa Polo",Preco=110.99M, TipoProduto = vestuario },
                            new Produto(){Nome="Camisa Regata",Preco=130.99M, TipoProduto = vestuario },
                            new Produto(){Nome="Caderno 10M",Preco=30.45M, TipoProduto = papelaria },
                            new Produto(){Nome="Papel Higienico",Preco=40.30M, TipoProduto = higiene },
                        }
                        
                        );
                }

                context.SaveChanges();
            }
        }

    }
}
