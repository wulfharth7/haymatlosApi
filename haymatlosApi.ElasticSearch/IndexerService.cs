using haymatlosApi.haymatlosApi.Models;
using haymatlosApi.haymatlosApi.Utils.Pagination;
using haymatlosApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace haymatlosApi.haymatlosApi.ElasticSearch
{
    public class IndexerService
    {
        private readonly JsonSerializerSettings _options = new() { NullValueHandling = NullValueHandling.Ignore,
                                                                    /*ReferenceLoopHandling = ReferenceLoopHandling.Ignore */
                                                                                                            };

        private PostgresContext _context;
        public IndexerService(PostgresContext postgresContext)
        {
            _context = postgresContext;
        }
        public async Task indexData() //this service is used to put all data into a json file so i can use it with elastic service
                                      //of course it only works with small data right now. big data would crush this down into pieces. i'll improve it later after i get a better grasp on elastic.
        {
            var validFilter = new PaginationFilter();
            var users = await _context.Users
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();

            var editedUsers = new List<User>();
            foreach (var user in users)
            {
                var posts = await _context.Posts
                    .Where(d => d.FkeyUuidUser.Equals(user.Uuid))
                    .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize)
                    .ToListAsync();// this should iterate the whole posts later. the comments dont work yet.

                var editedPosts = new List<Post>();
                foreach (var post in posts)
                {
                    editedPosts.Add(new Post { Title = post.Title, PkeyUuidPost = post.PkeyUuidPost });
                }

                editedUsers.Add(new User { Uuid = user.Uuid, Nickname = user.Nickname, Posts = editedPosts });
            }
            await SimpleWrite(editedUsers);
        }


        public async Task SimpleWrite(List<User> obj, string fileName = "jsonfile.json")
        {
            using (StreamWriter outputFile = new StreamWriter(fileName))
            {
                foreach (var user in obj)
                {
                    var jsonString = JsonConvert.SerializeObject(user, _options);
                    await outputFile.WriteLineAsync("\"{\\\"index\\\": {}}\"");
                    await outputFile.WriteLineAsync(jsonString);
                }
            }
        }

    }
}
//https://www.elastic.co/guide/en/kibana/current/data-views.html
//https://stackoverflow.com/questions/15936616/import-index-a-json-file-into-elasticsearch
//      curl -X POST --insecure -u "elastic:gx8K88YIQijE7M4X6tWd" "https://localhost:9200/test_index/_bulk" -H "Content-Type: application/json" --data-binary "@jsonfile.json"
//i'll also optimize this place and wont use this cmd command.
//this was fucking tiring but really was worth it. im kinda understanding it now.
//for elastic search. i created index with tis bcs array of objs aren't acepted so i nested em.
/*PUT /test_index
{
   "mappings": {
      "properties": {
        "Uuid": {
          "type": "text"
        },
        "Nickname":{
          "type": "text"
        },
         "Posts": {
            "type": "nested",
            "properties": {
               "PkeyUuidPost": {
                  "type": "keyword"
               },
               "Title": {
                  "type": "text"
               }
            }
         }
      }
   }
}
}*/
//https://www.elastic.co/guide/en/elasticsearch/reference/8.9/array.html
// just had to type index name into the dataview index pattern :D
//https://www.elastic.co/guide/en/elasticsearch/client/net-api/8.9/connecting.html#single-node