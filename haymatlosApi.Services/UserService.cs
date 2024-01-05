using haymatlosApi.haymatlosApi.Models;
using haymatlosApi.haymatlosApi.Utils;
using haymatlosApi.haymatlosApi.Utils.Objects;
using haymatlosApi.haymatlosApi.Utils.Pagination;
using haymatlosApi.Utils;
using Microsoft.EntityFrameworkCore;
//e1705d38-1b60-43df-ba64-af205dd8205e

namespace haymatlosApi.Services
{
    public class UserService
    {
        private readonly PostgresContext _context;
        private readonly PostService _postService;
        private readonly tokenUtil _tokenUtil;
        private readonly UriService _uriService;
        private readonly CommentService _commentService;
        private readonly string passwordSalt = PasswordHashing.GenerateSalt();

        public UserService(PostgresContext postgresContext, tokenUtil tokenutil, PostService postservice, CommentService commentservice, UriService uriService)
        {
            _context = postgresContext;
            _tokenUtil = tokenutil;
            _postService = postservice;
            _commentService = commentservice;
            _uriService = uriService;
        }

        public async Task<PaginatedResponse<IEnumerable<User>>> getUsers(PaginationFilter filter, string route)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedUserData = await _context.Users
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            var totalRecords = await _context.Users.CountAsync();
            var pagedResponse = PaginationHelper.CreatePagedReponse<User>(pagedUserData, validFilter, totalRecords, _uriService, route);
            return pagedResponse;
        }

        public async Task<ResponseResult<User>> getUserById(Guid userId, bool getPosts = false)
        {
            var user = await _context.Users.Where(d => d.Uuid.Equals(userId)).FirstOrDefaultAsync();
            return new ResponseResult<User>(user); 
        }

        public async Task<bool> registerUser(string nickname, string password) //maybe email
        {
            var userExists = await _context.Users.Where(d => d.Nickname.Equals(nickname)).FirstOrDefaultAsync();
            if (userExists != null)
            {
                return false;
            }
            else
            {
                try 
                {
                    var user = new ObjectFactoryUser<User>().createUserObj(nickname, password, passwordSalt); //null checker will do more stuff here.
                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch 
                {
                    return false;
                }
            }
        }

        public async Task<User> updateuser(Guid uuid, User newUser)
        {
            var user = await _context.Users.Where(d => d.Uuid.Equals(uuid)).FirstOrDefaultAsync();
           /* if (!NullChecker.IsNull(user))
            {
                return null;
            }*/
            user.Nickname = newUser.Nickname; //this should go into the object factory too.
            user.Password = newUser.Password;
            user.Role = newUser.Role;
            user.Token = newUser.Token;
            await _context.SaveChangesAsync();
            return new User { Uuid = user.Uuid, Nickname = user.Nickname, Role = user.Role, Token = user.Token};
        }

        public async Task<object?> deleteUser(Guid pkey_uuid)
        {
            var user = await _context.Users.Where(d => d.Uuid.Equals(pkey_uuid)).FirstOrDefaultAsync();
            if (!NullChecker.IsNull(user))
            {
                _context.Users.Remove(user);    //isnt this blocking a thread?
                await _context.SaveChangesAsync();
                return user.Uuid;
            }
            return null;
        }

        //check how to use cancellation token.
        public async Task<User> loginUser(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Nickname == username);
            //check null
            var passwordHash = PasswordHashing.ComputeHash(password, user.Salt);
            if (user.Password == passwordHash)
            {
                var token = await _tokenUtil.createToken(user);
                user.Token = token;
                return await updateuser(user.Uuid, user);
            }
            else
            {
                return new User { };
            }

        }       
    }
}
