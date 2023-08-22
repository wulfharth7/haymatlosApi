using haymatlosApi.haymatlosApi.Models;
using haymatlosApi.haymatlosApi.Utils;
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

        public async Task<ICollection<Post>> getPostsOfUser(Guid userId)
        {
            var posts = await _context.Posts.Where(x => x.FkeyUuidUser.Equals(userId)).ToListAsync();
            return posts;
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
