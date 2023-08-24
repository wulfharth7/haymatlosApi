using haymatlosApi.haymatlosApi.Models;
using haymatlosApi.Services;
using Newtonsoft.Json;

namespace haymatlosApi.haymatlosApi.ElasticSearch
{
    public static class JsonFileUtils
    {
        private static readonly JsonSerializerSettings _options
            = new() { NullValueHandling = NullValueHandling.Ignore,
                /*ReferenceLoopHandling = ReferenceLoopHandling.Ignore */      // [JsonProperty(ignore and stuff)]
            };
 
        public static async Task SimpleWrite(List<User> obj, string fileName = "jsonfile.json")
        {
            using (StreamWriter outputFile = new StreamWriter(fileName))
            {
                foreach (var user in obj)
                {
                    var jsonString = JsonConvert.SerializeObject(user, _options);
                    await outputFile.WriteLineAsync(jsonString );
                }
            }
            /*File.WriteAllText(fileName, jsonString); //make it async*/
        }
    }
}
