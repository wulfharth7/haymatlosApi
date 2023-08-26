using haymatlosApi.Controllers;
using haymatlosApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace haymatlosApi.haymatlosApi.ElasticSearch.Api
{
    [Route("/ElasticSearch")]
    [AllowAnonymous]
    [ApiController]
    public class ElasticSearchApi : ControllerBase
    {
        readonly IndexerService _indexerService;
        readonly ElasticService _elasticService;
        public ElasticSearchApi(IndexerService indexerservice, ElasticService elastic) {
            _indexerService = indexerservice;
            _elasticService = elastic;
        } 

        [HttpGet("indexData")]
        [Authorize(Roles = "admin")]
        public async Task writeDataIntoJsonDocument()
        {
            await _indexerService.indexData();
        }

        [HttpGet("fullTextSearch")]
        [Authorize(Roles = "user")]
        public async Task<List<Document>> fullTextSearch(string searchQuery)
        {
            return await _elasticService.fullTextSearch("test_index",searchQuery);
        }
    }
}
