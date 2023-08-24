using haymatlosApi.haymatlosApi.Models;
using haymatlosApi.Services;
using Newtonsoft.Json.Linq;

namespace haymatlosApi.haymatlosApi.Utils
{
    public static class objectsFromJsonData         
    {
        //i didn't like this function. the name doesn't state the functionality clearly. and also i think i dont have to send post and comment seperately, probably virtual navigation properties would help. but i'll check this later.
        public static (Post,Comment) PostAndComment (JObject postAndCommentDatafromJson)
        {
            if (postAndCommentDatafromJson.TryGetValue("post", out JToken postToken) && postAndCommentDatafromJson.TryGetValue("comment", out JToken commentToken))
            {
                var post = postToken.ToObject<Post>();
                var comment = commentToken.ToObject<Comment>();
                return (post, comment);
            }
            return (null, null);
        }
    }
}
