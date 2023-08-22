using haymatlosApi.haymatlosApi.Models;
using haymatlosApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace haymatlosApi.Controllers
{
    [Route("/comments/")]
    [ApiController]
    public class CommentApiRouter : ControllerBase
    {
        readonly CommentService _commentService;
        public CommentApiRouter(CommentService commentService) => _commentService = commentService;

        //POST:
        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task/*<Comment>*/ createComment(Guid postId, Comment comment, Guid? parentComment)
        {
            await _commentService.createComment(postId, comment, parentComment);
        }

        [HttpPut]
        [Authorize(Roles = "admin,user")]
        public async Task deleteComment(Guid commentId)
        {
            await _commentService.deleteComment(commentId);
        }
    }
}
