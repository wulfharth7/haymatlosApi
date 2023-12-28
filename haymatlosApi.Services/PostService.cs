using haymatlosApi.haymatlosApi.Models;
using haymatlosApi.haymatlosApi.Utils.Pagination;
using Microsoft.AspNetCore.Routing;
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
