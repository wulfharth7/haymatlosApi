using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using haymatlosApi.haymatlosApi.Models;
using Nest;

namespace haymatlosApi.haymatlosApi.ElasticSearch
{
    public class Postx
    {
        public string PkeyUuidPost { get; set; }
        public string Title { get; set; }
    }

    public class Document
    {
        public string Uuid { get; set; }
        public string Nickname { get; set; }
        public List<Postx> Posts { get; set; }
    }

    public class ElasticService
    {
        private readonly ElasticsearchClient _client;
        public ElasticService()
        {
            var settings = new ElasticsearchClientSettings(new Uri("https://localhost:9200"))
                            .CertificateFingerprint("517065d9abea1d88208cb6755f187e53110d4fb70da35f53b3d4214e84dd3de9")
                            .Authentication(new BasicAuthentication("elastic", "IBM=5L-kCO79sYb28NLV"));

            var client = new ElasticsearchClient(settings);
            _client = client;
        }

        public async Task<List<Document>> fullTextSearch(string indexName, string searchText)
        {
            var searchResponse = await _client.SearchAsync<Document>(s => s
                .Index(indexName)
                .Query(q => q
                    .Nested(n => n
                        .Path(p => p.Posts)
                        .Query(nq => nq
                            .Match(m => m
                                .Field(f => f.Posts[0].Title)
                                .Query(searchText)
                            )
                        )
                    )
                )
            );

            if (searchResponse != null)
            {
                return searchResponse.Documents.ToList();
            }

            return new List<Document>();
        }
    }
}
