using Microsoft.WindowsAzure.Storage;
using System;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;

namespace Storage
{
    class Program
    {
        static void Main(string[] args)
        {
            var connString = "DefaultEndpointsProtocol=https;AccountName=stxcorpvm;AccountKey=pMYKxUsIG0kq39FGqOO6rAd2w8nqBVhTVntrrC7DSd3Cr2w6pSN2hMIj6dMctmzpfkOIOHmHupjUFoHe4uQT1g==;EndpointSuffix=core.windows.net";

            var conn = CloudStorageAccount.Parse(connString);

            var client = conn.CreateCloudBlobClient();
            var container = client.GetContainerReference("imagens");
            container.CreateIfNotExistsAsync();

            options(container);

            Console.WriteLine("Fim");
            Console.ReadLine();
        }

        private static async void Download(CloudBlobContainer container)
        {
            Console.Write("Informe o nome do arquivo: ");
            var file = Console.ReadLine();

            Console.WriteLine("Baixando arquivo");

            var blob = container.GetBlockBlobReference(file);
            var path = $"f:\files\new-{new Random().Next(int.MaxValue)}.jpg";

            using (var fs = File.OpenWrite(path))
            {
                await blob.DownloadToStreamAsync(fs);
            }
            Console.WriteLine("Arquivo recebido");
        }

        private static async void Upload(CloudBlobContainer container)
        {
            Console.WriteLine("Enviando arquivo");
            var blob = container.GetBlockBlobReference($"img-{new Random().Next(int.MaxValue)}.jpg");

            using (var fs = File.OpenRead(@"f:\files\logo_spfc.jpg"))
            {
                await blob.UploadFromStreamAsync(fs);
            }
            Console.WriteLine("Arquivo enviado");
        }

        private static void options(CloudBlobContainer container)
        {
            int op = 0;
            bool sair = true;

            do
            {
                Console.WriteLine("Olá, digite a opção desejada.");
                Console.WriteLine("1. Upload de arquivo.");
                Console.WriteLine("2. Download de arquivo.");
                
                Console.WriteLine("0. Digite 0 para sair.");

                Console.Write("Opção: ");
                int.TryParse(Console.ReadLine(), out op);

                Console.Clear();
                switch (op)
                {
                    case 1:
                        Console.WriteLine("Cadastrar Aluno");
                        Console.WriteLine("");
                        Upload(container);
                        break;
                    case 2:
                        Console.WriteLine("Aqui estão todos os Alunos");
                        Console.WriteLine("");
                        Download(container);
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
