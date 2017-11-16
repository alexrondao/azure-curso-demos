using CadCli.Data;
using CadCli.Models;
using Newtonsoft.Json;
using System;

namespace CadCli.Service.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Aperte enter p/ processar fila...");
            var service = new QueueService();
            var msg = service.Obter();
            if (msg != null)
            {
                processarMensagem(msg);
            }
        }

        private static void processarMensagem(string msg)
        {
            var pedido = JsonConvert.DeserializeObject<Pedido>(msg);
            using (var ctx = new CadCliDataContext())
            {

            }
        }
    }
}
