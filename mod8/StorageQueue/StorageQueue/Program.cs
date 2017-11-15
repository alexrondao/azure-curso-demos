using System;

namespace StorageQueue
{
    class Program
    {
        static QueueService service = new QueueService();

        static void Main(string[] args)
        {
            service.GerarCarga();
            options();

            Console.WriteLine("Fim");
            Console.ReadLine();
            
        }

        private static void options()
        {
            int op = 0;
            bool sair = true;

            do
            {
                Console.WriteLine("Olá, digite a opção desejada.");
                Console.WriteLine("1. Enviar uma mensagem.");
                Console.WriteLine("2. Enviar mensagem randomica.");
                Console.WriteLine("3. Espiar a mensagen.");
                Console.WriteLine("4. Obter a mensagen.");
                Console.WriteLine("5. Excluir a mensagen.");
                Console.WriteLine();
                Console.WriteLine("0. Digite 0 para sair.");

                Console.Write("Opção: ");
                int.TryParse(Console.ReadLine(), out op);

                Console.Clear();
                switch (op)
                {
                    case 1:
                        Console.Write("Escreva sua mensagem:");
                        string msg = Console.ReadLine();
                        service.Enviar(msg);
                        break;
                    case 2:
                        Console.WriteLine("Enviando mensagem randomica...");
                        service.Enviar(new Random().Next(int.MaxValue).ToString());
                        //Download(container);
                        //Console.Write("Opção: ");
                        //int.TryParse(Console.ReadLine(), out op);
                        break;
                    case 3:
                        Console.WriteLine("Espiando mensagem...");
                        Console.WriteLine(service.Espiar());
                        Console.WriteLine("Mensagem espiada...");
                        break;

                    case 4:
                        Console.WriteLine("Obtendo mensagem...");
                        Console.WriteLine(service.Obter());
                        Console.WriteLine("Mensagem obtida...");
                        break;

                    case 5:
                        Console.WriteLine("Excluir mensagem");
                        Console.WriteLine(service.Excluir());
                        Console.WriteLine("Mensagem obtida...");
                        break;

                    default:
                        sair = false;
                        break;
                }
                Console.WriteLine("-----------------------");
                Console.WriteLine("");

            } while (sair);
        }
    }
}
