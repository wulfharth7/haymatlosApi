using haymatlosApi.haymatlosApi.Models;
using haymatlosApi.haymatlosApi.Utils.Pagination;
using haymatlosApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace haymatlosApi.Controllers
{
    [Route("/posts/")]
    [ApiController]
    [AllowAnonymous]
    public class PostsApiRouter : ControllerBase
    {
        readonly PostService _postService;
        public PostsApiRouter(PostService postService) => _postService = postService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<PaginatedResponse<IEnumerable<Post>>> getPostsOfUser(Guid userId, [FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            return await _postService.getPostsOfUser(userId, filter, route!); //not sure if this should be nullable or not.
        } 

        //POST:
        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<Post> createPost(Guid userId, Post post)
        {
            return await _postService.createPost(userId, post);
        }

        [HttpDelete]
        [Authorize(Roles = "admin,user")]
        public async Task deletePost(Guid postId)
        {
            await _postService.deletePost(postId);
        }
    }
}
