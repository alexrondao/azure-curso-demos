using System;

namespace DemoAzureTableStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            int op = 0;
            bool sair = true;

            do
            {
                
                Console.WriteLine("Olá, digite a opção desejada.");
                Console.WriteLine("1. Adicionar um Aluno.");
                Console.WriteLine("2. Mostrar todos os Alunos cadastrados.");
                Console.WriteLine("3. Pesquisar por Sobrenome.");
                Console.WriteLine("4. Pesquisar por sobrenome e id.");
                Console.WriteLine("5. Deletar o aluno.");
                Console.WriteLine("0. Digite 0 para sair.");

                Console.Write("Opção: ");
                int.TryParse(Console.ReadLine(), out op);

                Console.Clear();
                switch (op)
                {
                    case 1:
                        Console.WriteLine("Cadastrar Aluno");
                        Console.WriteLine("");
                        AddAluno();
                        break;
                    case 2:
                        Console.WriteLine("Aqui estão todos os Alunos");
                        Console.WriteLine("");
                        GetAll();
                        break;
                    case 3:
                        Console.WriteLine("Achei para você, veja.");
                        Console.WriteLine("");
                        GetbyPartitionKey();
                        break;
                    case 4:
                        Console.WriteLine("Achei, olha ele ai.");
                        Console.WriteLine("");
                        GetItem();
                        break;
                    case 5:
                        Console.WriteLine("Puts, ele se foi, agora num já era.");
                        Console.WriteLine("");
                        Delete();
                        break;
                    default:
                        sair = false;
                        break;
                }
                Console.WriteLine("-----------------------");
                Console.WriteLine("");

            } while (sair);

            Console.WriteLine("Fim");
            Console.ReadLine();
        }

        private static void Delete()
        {
            var storage = new TableStorageRep();

            Console.Write("Informe o Sobrenome: ");
            var sobrenome = Console.ReadLine();

            Console.Write("Informe o Id: ");
            var id = Console.ReadLine();

            var a = storage.GetItem(sobrenome, id).Result;

            if (a == null)
            {
                Console.WriteLine("Aluno não encontrado, não foi deletado.");
                return;
            }

            storage.Delete(a).Wait();

        }

        private static void GetItem()
        {
            var storage = new TableStorageRep();

            Console.Write("Informe o Sobrenome: ");
            var sobrenome = Console.ReadLine();

            Console.Write("Informe o Id: ");
            var id = Console.ReadLine();

            var a = storage.GetItem(sobrenome, id).Result;

            if (a == null)
            {
                Console.WriteLine("Aluno não encontrado.");
                return;
            }
            Console.WriteLine($"Id: {a.RowKey} - Sobrenome: {a.PartitionKey} - Nome: {a.Nome} - Idade: {a.Idade}");
        }

        private static void GetbyPartitionKey()
        {
            var storage = new TableStorageRep();

            Console.Write("Informe o Sobrenome: ");
            var sobrenome = Console.ReadLine();

            var alunos = storage.GetAll(sobrenome).Result;

            alunos.ForEach(a => Console.WriteLine($"Id: {a.RowKey} - Sobrenome: {a.PartitionKey} - Nome: {a.Nome} - Idade: {a.Idade}"));
        }

        private static void GetAll()
        {
            var storage = new TableStorageRep();
            var alunos = storage.GetAll().Result;

            alunos.ForEach(a => Console.WriteLine($"Id: {a.RowKey} - Sobrenome: {a.PartitionKey} - Nome: {a.Nome} - Idade: {a.Idade}"));
        }

        private static void AddAluno()
        {
            int idade = 0;

            Console.Write("Nome: ");
            var nome = Console.ReadLine();

            Console.Write("Sobrenome: ");
            var sobrenome = Console.ReadLine();

            Console.Write("Idade: ");
            int.TryParse(Console.ReadLine(), out idade);

            var storage = new TableStorageRep();
            var aluno = new Aluno(sobrenome, nome, idade);

            storage.Insert(aluno).Wait();

            Console.WriteLine("Aluno cadastrado");
        }
    }
}
