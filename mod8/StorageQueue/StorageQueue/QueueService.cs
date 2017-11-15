using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace StorageQueue
{
    public class QueueService
    {
        private readonly CloudStorageAccount _storageAccount =
            CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=stxcorpvm;AccountKey=pMYKxUsIG0kq39FGqOO6rAd2w8nqBVhTVntrrC7DSd3Cr2w6pSN2hMIj6dMctmzpfkOIOHmHupjUFoHe4uQT1g==;EndpointSuffix=core.windows.net");

        private const string __MINHA_FILA = "filateste";

        private async Task<CloudQueue> ObterFila(string nome)
        {
            string novoNome = nome.Replace("-", "").Replace(".", "").ToLower();

            var queueClient = _storageAccount.CreateCloudQueueClient();
            var queue = queueClient.GetQueueReference(novoNome);

            await queue.CreateIfNotExistsAsync();
            return queue;
        }

        public void Enviar(string msg)
        {
            var queue = ObterFila(__MINHA_FILA).Result;
            queue.AddMessageAsync(new CloudQueueMessage(msg));
        }

        public string Espiar()
        {
            var queue = ObterFila(__MINHA_FILA).Result;
            var msg = queue.PeekMessageAsync().Result;
            return msg?.AsString ?? "Mensagem não encontrada";
        }

        public string Obter()
        {
            var queue = ObterFila(__MINHA_FILA).Result;
            var msg = queue.GetMessageAsync().Result;
            return msg?.AsString ?? "Mensagem não encontrada";
        }

        public string Excluir()
        {
            var queue = ObterFila(__MINHA_FILA).Result;
            var msg = queue.GetMessageAsync().Result;
            if (msg != null)
            {
                var texto = msg.AsString;
                queue.DeleteMessageAsync(msg);
                return texto;
            }
            return "Mensagem não encontrada";
        }

        public void GerarCarga()
        {
            //var w = new StreamWriter($@"f:\files\msgs.txt");

            for (int i = 0; i < 1000000; i++)
            {
                Enviar(new Random().Next(int.MaxValue).ToString());
            }
        }
    }
}
