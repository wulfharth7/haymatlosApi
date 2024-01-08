using haymatlosApi.haymatlosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace haymatlosApi.Services
{
    public class CommentService
    {
        private PostgresContext _context;
        public CommentService(PostgresContext postgresContext)
        {
            _context = postgresContext;
        }

       /* public async Task getCommentsAndChild(Guid postId)
        {
       first 35 comments lets say.
        }*/

        public async Task createComment(Post post, Comment comment, Guid? parentComment)
        {
            var cm1 = new ObjectFactoryComment<Comment>().createCommentObj(post,comment, parentComment);
            await _context.Comments.AddAsync(cm1);
            await _context.SaveChangesAsync();
        }

        public async Task deleteComment(Guid commentId)
        {
            var comment = await _context.Comments.Where(d => d.PkeyUuidComment.Equals(commentId)).FirstOrDefaultAsync();
            comment.Description = "this comment has been deleted.";
            await _context.SaveChangesAsync();
            //return post uuid
        }

        public async Task<IEnumerable<Comment>> getUserComments(Guid userId)
        {
            var comments = await _context.Comments.Where(id => id.FkeyUuidUser.Equals(userId)).ToListAsync();
            return comments;
        }

        public async Task<IEnumerable<Comment>> getPostComments(Guid PostId)
        {
            var comments = await _context.Comments.Where(id => id.FkeyUuidPost.Equals(PostId) && id.ParentComment == null).ToListAsync();
            return comments;
        }
    }
}
