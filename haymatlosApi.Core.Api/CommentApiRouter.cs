﻿using haymatlosApi.haymatlosApi.Models;
using haymatlosApi.haymatlosApi.Utils;
using haymatlosApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Reflection.Metadata.Ecma335;

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
        public async Task<IEnumerable<Comment>> getCommentsOfAPost(Guid postId)
        {
            return await _commentService.getPostComments(postId);
        }

        [HttpGet("userId")]
        [Authorize(Roles = "user")]
        public async Task<IEnumerable<Comment>>getCommentsOfUser(Guid userId)
        {
            return await _commentService.getUserComments(userId);
        }

        [HttpDelete]
        [Authorize(Roles = "admin,user")]
        public async Task deleteComment(Guid commentId)
        {
            await _commentService.deleteComment(commentId);
        }
    }
}
