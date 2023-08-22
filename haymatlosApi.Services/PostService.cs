using haymatlosApi.haymatlosApi.Models;
using haymatlosApi.haymatlosApi.Utils;
using haymatlosApi.haymatlosApi.Utils.haymatlosApi.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace haymatlosApi.Services
{
    public class PostService
    {
        private PostgresContext _context;
        public PostService(PostgresContext postgresContext)
        {
            _context = postgresContext; 
        }

        public async Task<Post> createPost(Guid userId, Post post)
        {
            Post post1 = new Post()
            {
                PkeyUuidPost = Guid.NewGuid(),
                FkeyUuidUser = userId,
                Title = post.Title,
                IsIndexed = false,
            };
            await _context.Posts.AddAsync(post1);
            await _context.SaveChangesAsync();
            return post1;
        }

        public async Task<PaginatedResponse<IEnumerable<Post>>> getPostsOfUser(Guid userId, PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedPostData = await _context.Posts.Where(x => x.FkeyUuidUser.Equals(userId))
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            return new PaginatedResponse<IEnumerable<Post>>(pagedPostData, validFilter.PageNumber, validFilter.PageSize);
        }

        public async Task deletePost(Guid postId)
        {
            var post = await _context.Posts.Where(d => d.PkeyUuidPost.Equals(postId)).FirstOrDefaultAsync();
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            //return post uuid
        }
    }
}
