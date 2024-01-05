using haymatlosApi.haymatlosApi.Models;
using haymatlosApi.haymatlosApi.Utils.Objects;
using haymatlosApi.haymatlosApi.Utils.Pagination;
using Microsoft.EntityFrameworkCore;

namespace haymatlosApi.Services
{
    public class PostService
    {
        private PostgresContext _context;
        private readonly UriService _uriService;

        public PostService(PostgresContext postgresContext, UriService service)
        {
            _context = postgresContext;
            _uriService = service;
        }

        public async Task<Post> createPost(Guid userId, Post post)
        {
            var post1 = new ObjectFactoryPost<Post>().createPostObj(userId, post);
            await _context.Posts.AddAsync(post1);
            await _context.SaveChangesAsync();
            return post1;
        }

        public async Task<Post> updatePost(Guid uuid, Post newPost)
        {
            var post = await _context.Posts.Where(d => d.PkeyUuidPost.Equals(uuid)).FirstOrDefaultAsync();
            /* if (!NullChecker.IsNull(user))
             {
                 return null;
             }*/
            post.Title = newPost.Title;
            post.category = newPost.category;
            post.imageUrl = newPost.imageUrl;
            post.content = newPost.content;//this should go into the object factory too.

            await _context.SaveChangesAsync();
            return new Post { PkeyUuidPost = post.PkeyUuidPost, Title = post.Title, category = post.category, content = post.content, imageUrl = post.imageUrl };
        }

        public async Task<ResponseResult<Post>> getPostsById(Guid postId)
        {
            var post = await _context.Posts.Where(d => d.PkeyUuidPost.Equals(postId)).FirstOrDefaultAsync();
            return new ResponseResult<Post>(post);
        }
        public async Task<PaginatedResponse<IEnumerable<Post>>> getPosts(PaginationFilter filter, string route)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedPostData = await _context.Posts
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            var totalRecords = await _context.Posts.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<Post>(pagedPostData, validFilter, totalRecords, _uriService, route);
            return pagedReponse;
        }

        public async Task<PaginatedResponse<IEnumerable<Post>>> getPostsOfUser(Guid userId, PaginationFilter filter, string route)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedPostData = await _context.Posts
                .Where(x => x.FkeyUuidUser.Equals(userId))
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            var totalRecords = await _context.Posts.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<Post>(pagedPostData, validFilter, totalRecords, _uriService, route);
            return pagedReponse;
        }

        public async Task<PaginatedResponse<IEnumerable<Post>>> getPostsOfACategory(string category, PaginationFilter filter, string route)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedPostData = await _context.Posts
                .Where(x => x.category.Equals(category))
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            var totalRecords = await _context.Posts.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<Post>(pagedPostData, validFilter, totalRecords, _uriService, route);
            return pagedReponse;
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
