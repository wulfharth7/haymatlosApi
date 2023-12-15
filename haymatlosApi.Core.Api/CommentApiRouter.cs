using haymatlosApi.haymatlosApi.Models;
using haymatlosApi.haymatlosApi.Utils;
using haymatlosApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Data;

namespace haymatlosApi.Controllers
{
    public class PostAndComment
    {
        public Post post { get; set; }
        public Comment comment { get; set; }
    }

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
        public async Task/*<Comment>*/ createComment(PostAndComment model, Guid? parentComment)
        {
            await _commentService.createComment(model.post, model.comment, parentComment);
        }

        [HttpGet]
        [Authorize(Roles = "user")]
        public async Task<IEnumerable<Comment>> getCommentsOfAPost(Guid PostId)
        {
            return await _commentService.getPostComments(PostId);
        }

        /*[HttpGet]
        [Authorize(Roles = "user")]
        public async Task*//*Comment List Döner sanırsam emin değilim suan*//* getCommentsOfUser(Guid UserId)
        {
            //service already ready. call it here.
        }*/

        [HttpPut]
        [Authorize(Roles = "admin,user")]
        public async Task deleteComment(Guid commentId)
        {
            await _commentService.deleteComment(commentId);
        }
    }
}
