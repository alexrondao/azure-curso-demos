using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusQueue.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var stringConn = "Endpoint=sb://demomod8fan.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=lVIYgniqTGQVXLe79jra0O8nXESle8Gos4IrpR5h2rE=";
            var nomeFila = "produtos";

            var server = QueueClient.CreateFromConnectionString(stringConn, nomeFila);
            var flag = true;

            while (flag)
            {
                var produtos = new List<Produto> {
                    new Produto{ Nome=$"prod{new Random().Next(100)}", Preco=9.99M},
                    new Produto{ Nome=$"prod{new Random().Next(100)}", Preco=91.99M},
                    new Produto{ Nome=$"prod{new Random().Next(100)}", Preco=90.99M},
                };


                var msg = JsonConvert.SerializeObject(produtos);
                server.Send(new BrokeredMessage(msg));
                Console.WriteLine("Mensagem Enviada");
                Console.Write("Deseja enviar uma nova mensagem?(s/n)");
                flag = Console.ReadLine().ToLower() == "s";
            }
            Console.WriteLine("Fim");
            Console.ReadLine();
        }
    }


}
