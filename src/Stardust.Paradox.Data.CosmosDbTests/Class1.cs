using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Azure.Graphs;
using Microsoft.Azure.Graphs.Elements;
using Newtonsoft.Json.Linq;
using Stardust.Paradox.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stardust.Paradox.CosmosDbTest
{
    public class Class1 : IGremlinLanguageConnector
    {
        private static DocumentClient _client;
        private static ResourceResponse<DocumentCollection> _graph;

        public Class1()
        {
            if (_client == null)
                _client = new DocumentClient(
                    new Uri("https://jonas-graphtest.documents.azure.com:443/"),
                    "1TKgMc0u6F0MOBQi4jExGm1uAfOMHcXxylcvL55qV7FiCKx5LhTIW0FVXvJ68zdzFnFaS58yPtlxmBLmbDka1A==");
        }

        //public IEnumerable<TData> Execute<TData>(string query)
        //{
        //    return Task.Run(async () => await ExecuteAsync<TData>(query).ConfigureAwait(false)).GetAwaiter().GetResult();

        //}

        //public async Task<IEnumerable<TData>> ExecuteAsync<TData>(string query)
        //{
        //    var graph = await DocumentCollection().ConfigureAwait(false);
        //    var gremlinQ = _client.CreateGremlinQuery<Vertex>(graph, query);
        //    var d = await gremlinQ.ExecuteNextAsync<Vertex>().ConfigureAwait(false);
        //    return d.Select(i => new JObject(i.GetVertexProperties().Select(s => new JProperty(s.Key, s.Value))))
        //        .Select(jObj => (TData) jObj.ToObject(typeof(TData))).ToList();
        //}

        public async Task<IEnumerable<dynamic>> ExecuteAsync(string query)
        {
            var graph = await DocumentCollection().ConfigureAwait(false);
            var gremlinQ = _client.CreateGremlinQuery(graph, query);
            var d = await gremlinQ.ExecuteNextAsync().ConfigureAwait(false);
            return d.AsEnumerable();
        }

        private async Task<DocumentCollection> DocumentCollection()
        {
            if (_graph != null) return _graph;
            Database database = await _client.CreateDatabaseIfNotExistsAsync(new Database { Id = "graphTest" }).ConfigureAwait(false);
            _graph = await _client.CreateDocumentCollectionIfNotExistsAsync(
                UriFactory.CreateDatabaseUri("graphTest"),
                new DocumentCollection { Id = "services" },
                new RequestOptions { OfferThroughput = 1000 }).ConfigureAwait(false);
            return _graph;
        }
    }
}