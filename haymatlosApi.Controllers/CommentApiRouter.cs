using haymatlosApi.haymatlosApi.Models;
using haymatlosApi.haymatlosApi.Utils;
using haymatlosApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Data;

namespace haymatlosApi.Controllers
{
    [Route("/comments/")]
    [AllowAnonymous]
    [ApiController]
    public class CommentApiRouter : ControllerBase
    {
        readonly CommentService _commentService;
        public CommentApiRouter(CommentService commentService) => _commentService = commentService;

        //POST:
        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task/*<Comment>*/ createComment(JObject postAndCommentDatafromJson, Guid? parentComment)
        {
            var post_and_comment = objectsFromJsonData.PostAndComment(postAndCommentDatafromJson);
            await _commentService.createComment(post_and_comment.Item1, post_and_comment.Item2, parentComment);
        }
        //i did this bcs turns out the endpoint doesn't like two different complicated classes as parameters. I had to send them from the JObj but this will be improved. just making it work for now.
        [HttpPut]
        [Authorize(Roles = "admin,user")]
        public async Task deleteComment(Guid commentId)
        {
            await _commentService.deleteComment(commentId);
        }
    }
}
