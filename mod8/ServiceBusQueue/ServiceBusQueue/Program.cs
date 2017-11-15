using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceBusQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            

            string stringConnection = "Endpoint=sb://xcorpebs.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=tXNtrPY9Pvt+xo6rmEvfHBSBnxXarwMamHINdTWd/Cs=";
            var nomeFila = "produtos";

            var server = QueueClient.CreateFromConnectionString(stringConnection, nomeFila);
            var flag = true;

            while (flag)
            {
                var produtos = new List<Produto>
                {
                    new Produto{Nome="prod1", Preco=9.99M},
                    new Produto{Nome="prod2", Preco=91.99M},
                    new Produto{Nome="prod3", Preco=90.99M}
                };

                var msg = JsonConvert.SerializeObject(produtos);
                server.Send(new BrokeredMessage(msg));
                Console.WriteLine("Mensagem enviada....");
                Console.Write("Deseja enviar mais uma mensagem? (s/n) ");
                flag = Console.ReadLine().ToLower() == "s";

            }

            //server. JsonConvert.SerializeObject(produtos);

            Console.WriteLine("Fim");
            Console.ReadLine();
        }
    }
}
