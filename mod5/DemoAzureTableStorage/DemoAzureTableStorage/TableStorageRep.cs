using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoAzureTableStorage
{
    public class TableStorageRep
    {
        private CloudStorageAccount _storage;
        private CloudTableClient _tableClient;
        public CloudTable _table;

        public TableStorageRep()
        {
            _storage = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=stxcorpvm;AccountKey=pMYKxUsIG0kq39FGqOO6rAd2w8nqBVhTVntrrC7DSd3Cr2w6pSN2hMIj6dMctmzpfkOIOHmHupjUFoHe4uQT1g==;EndpointSuffix=core.windows.net");
            _tableClient = _storage.CreateCloudTableClient();

            _table = GetTableAsync().Result;
                
        }

        private async Task<CloudTable> GetTableAsync()
        {
            var t = _tableClient.GetTableReference("Alunos");
            await t.CreateIfNotExistsAsync();
            return t;
        }

        public async Task Insert(Aluno aluno)
        {
            var operation = TableOperation.Insert(aluno);
            await _table.ExecuteAsync(operation);
        }

        public async Task<List<Aluno>> GetAll()
        {
            var query = new TableQuery<Aluno>();
            var result = new List<Aluno>();
            TableContinuationToken token = null;

            do
            {
                var queryResult = await _table.ExecuteQuerySegmentedAsync(query, token);
                token = queryResult.ContinuationToken;
                result.AddRange(queryResult.Results);

            } while (token != null);

            return result;
        }

        public async Task<List<Aluno>> GetAll(string sobrenome)
        {
            var query = new TableQuery<Aluno>()
                .Where(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, sobrenome)
                );

            var result = new List<Aluno>();
            TableContinuationToken token = null;
            do
            {
                var queryResult = await _table.ExecuteQuerySegmentedAsync(query, token);
                token = queryResult.ContinuationToken;
                result.AddRange(queryResult.Results);

            } while (token != null);

            return result;
        }

        public async Task<Aluno> GetItem(string sobrenome, string id)
        {
            var operation = TableOperation.Retrieve<Aluno>(sobrenome, id);

            var aluno = await _table.ExecuteAsync(operation);
            return (Aluno)aluno.Result;
        }

        public async Task Delete(Aluno aluno)
        {
            var operation = TableOperation.Delete(aluno);

            await _table.ExecuteAsync(operation);
        }
    }
}
