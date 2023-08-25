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
        public ElasticSearchApi(IndexerService indexerservice) => _indexerService = indexerservice;

        [HttpGet("indexData")]
        [Authorize(Roles = "admin")]
        public async Task writeDataIntoJsonDocument()
        {
            await _indexerService.indexData();
        }
    }
}
