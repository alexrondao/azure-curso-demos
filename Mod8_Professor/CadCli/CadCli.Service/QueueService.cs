using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadCli.Service
{
    public class QueueService
    {
        private readonly CloudStorageAccount storageAccount =
            CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=demomod8fan;AccountKey=3DQgLCB+DuyJ6pj3D7vRLf74yRP8GRcAgFNAQJB3lum1+AzdOIMCIlNQwuQv/b7gYC3EkOIcuRnrSORjnzVwnQ==;EndpointSuffix=core.windows.net");
        private const string minhaFila = "pedidos";

        private async Task<CloudQueue> ObterFila(string nome)
        {
            var queueClient = storageAccount.CreateCloudQueueClient();

            //fila só letras minúsculas e números
            var queue = queueClient.GetQueueReference(nome);

            await queue.CreateIfNotExistsAsync();
            return queue;

        }

        public void Enviar(string msg)
        {
            var queue = ObterFila(minhaFila).Result;
            queue.AddMessageAsync(new CloudQueueMessage(msg));
        }

        public string Espiar()
        {
            var queue = ObterFila(minhaFila).Result;
            var msg = queue.PeekMessageAsync().Result;
            return msg?.AsString ?? null;
        }

        public string Obter()
        {
            var queue = ObterFila(minhaFila).Result;
            var msg = queue.GetMessageAsync().Result;

            return msg?.AsString ?? null;
        }

        public string Excluir()
        {
            var queue = ObterFila(minhaFila).Result;
            var msg = queue.GetMessageAsync().Result;
            if (msg != null)
            {
                var texto = msg.AsString;
                queue.DeleteMessageAsync(msg);
                return texto;
            }

            return "Mensagem não encontrada";
        }


    }
}
