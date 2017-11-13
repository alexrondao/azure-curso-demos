using CadCli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    new List<Cliente>()
                    {
                        new Cliente(){ Nome = "Alex", Idade = 36, Sexo = Sexo.Masculino },
                        new Cliente(){ Nome = "Agatha", Idade = 32, Sexo = Sexo.Feminino},
                        new Cliente(){ Nome = "Chris", Idade = 29, Sexo = Sexo.Masculino},
                        new Cliente(){ Nome = "Jill", Idade = 31, Sexo = Sexo.Feminino},
                        new Cliente(){ Nome = "Ryu", Idade = 35, Sexo = Sexo.Masculino},
                        new Cliente(){ Nome = "Ken", Idade = 34, Sexo = Sexo.Masculino},
                        new Cliente(){ Nome = "Leona", Idade = 25, Sexo = Sexo.Feminino},
                        new Cliente(){ Nome = "Blue Mary", Idade = 26, Sexo = Sexo.Feminino},
                        new Cliente(){ Nome = "Shaka", Idade = 22, Sexo = Sexo.Masculino},
                        new Cliente(){ Nome = "Atena", Idade = 23, Sexo = Sexo.Feminino}
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
