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
            var stringConn = "Endpoint=sb://demomod8fan.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=lVIYgniqTGQVXLe79jra0O8nXESle8Gos4IrpR5h2rE=";
            var nomeFila = "produtos";

            var server = QueueClient.CreateFromConnectionString(stringConn, nomeFila);
            server.OnMessage(msg =>{
                Console.WriteLine($"Id: {msg.MessageId}");
                var produtos = JsonConvert.DeserializeObject<List<Produto>>(msg.GetBody<string>());
                produtos.ForEach(p => Console.WriteLine($"Id: {p.Id} | Nome: {p.Nome} | Preço: {p.Preco}"));
            });

            Console.ReadLine();
        }
    }
}
