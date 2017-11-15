using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusQueue.Client
{
    class Program
    {
        

        static void Main(string[] args)
        {
            
            string stringConnection = "Endpoint=sb://xcorpebs.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=tXNtrPY9Pvt+xo6rmEvfHBSBnxXarwMamHINdTWd/Cs=";
            var nomeFila = "produtos";

            var server = QueueClient.CreateFromConnectionString(stringConnection, nomeFila);

            server.OnMessage(msg => {
                Console.WriteLine($"Id: {msg.MessageId}");
                var produtos = JsonConvert.DeserializeObject<List<Produto>>(msg.GetBody<string>());
                produtos.ForEach(p => Console.WriteLine($"Id: {p.Id} | Nome: {p.Nome} | Preço: {p.Preco}"));
            });

            Console.Write("FIM...");
            Console.ReadLine();
        }
    }
}
