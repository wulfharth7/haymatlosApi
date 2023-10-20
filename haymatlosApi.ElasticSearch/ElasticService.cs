using Elastic.Clients.Elasticsearch;
using Elastic.Transport;


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
                            .Authentication(new BasicAuthentication("elastic", "IBM=5L-kCO79sYb28NLV")) ;

            var client = new ElasticsearchClient(settings);
            _client = client;
        }
    
        public async Task/*<IReadOnlyCollection<Document>>*/ fullTextSearch(string indexName, string searchText, int page = 1, int pageSize = 5)
        {
            var searchResponse = await _client.SearchAsync<Document>(s => s
             .Index(indexName)
                .Query(q => q
                    .Bool(b => b
                        .Should(sh => sh
                            .MultiMatch(mm => mm
                                .Query(searchText)
                                .Fields("Nickname")
                                /*.Fuzziness(Fuzziness.Auto)*/
                            ),
                            sh => sh
                                .Nested(n => n
                                    .Path("Posts")
                                    .Query(nq => nq
                                        .MultiMatch(m => m
                                            .Query(searchText)
                                            .Fields("Posts.Title")
                                        )
                                    )
                                )
                        )
                    )
                )
            );
            var documents = searchResponse.Documents.ToList();
            Console.WriteLine(documents.Count);


        }
    }

    /*POST /test_index/_search
{
  "query": {
    "bool": {
      "should": [
        {
          "multi_match": {
            "query": "barisca",
            "fields": ["Nickname"],
            "fuzziness": "AUTO"
          }
        },
        {
          "nested": {
            "path": "Posts",
            "query": {
              "multi_match": {
                "query": "Dünyasında",
                "fields": ["Posts.Title"]
              }
            }
          }
        }
      ]
    }
  }
}*/
}
