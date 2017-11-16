using System;

namespace StorageQueue
{
    class Program
    {
        static QueueService service = new QueueService();

        static void Main(string[] args)
        {
            //EnviarMensagem();
            //EspionarMensagem();
            //ObterMensagem();
            ExcluirMensagem();
            Console.WriteLine("Fim");
            Console.ReadLine();
        }

        private static void ExcluirMensagem()
        {
            Console.WriteLine("Obtendo mensagem...");
            Console.WriteLine(service.Excluir());
            Console.WriteLine("Mensagem excluída...");
        }

        private static void ObterMensagem()
        {
            Console.WriteLine("Obtendo mensagem...");
            Console.WriteLine(service.Obter());
            Console.WriteLine("Mensagem obtida...");
        }

        private static void EspionarMensagem()
        {
            Console.WriteLine("Obtendo mensagem...");
            Console.WriteLine(service.Espiar());
            Console.WriteLine("Mensagem obtida...");
        }

        private static void EnviarMensagem()
        {
            Console.WriteLine("Enviando mensagem...");
            service.Enviar($"Mensagem - {new Random().Next(int.MaxValue)}");
            Console.WriteLine("Mensagem enviada");
        }
    }
}
