using haymatlosApi.haymatlosApi.Models;
using haymatlosApi.haymatlosApi.Utils;
using haymatlosApi.haymatlosApi.Utils.Pagination;
using haymatlosApi.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
// e1705d38-1b60-43df-ba64-af205dd8205e
namespace haymatlosApi.Services
{
    public class UserService
    {
        private readonly PostgresContext _context;
        private readonly PostService _postService;
        private readonly tokenUtil _tokenUtil;
        private readonly string passwordSalt = PasswordHashing.GenerateSalt();

        public UserService(PostgresContext postgresContext, tokenUtil tokenutil, PostService postservice)
        {
            _context = postgresContext; 
            _tokenUtil = tokenutil;
            _postService = postservice;
        } 

        public async Task<PaginatedResponse<IEnumerable<User>>> getUsers(PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedUserData = await _context.Users
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            return new PaginatedResponse<IEnumerable<User>>(pagedUserData, validFilter.PageNumber, validFilter.PageSize);
        }

        public async Task<User> getUserById(Guid userId, bool getPosts = false, PaginationFilter filter = null)
        {
            //ask if posts are needed for example.
            var user = await _context.Users.FindAsync(userId);
            if(getPosts == true) user.Posts = (ICollection<Post>)await _postService.getPostsOfUser(userId, filter);            //this shouldn't be needed probably?, i think my schema is wrong? i have to double check this
            return user;
        }

        public async Task registerUser(string nickname, string password)
        {
            var user = new ObjectFactoryUser<User>().createUserObj(nickname,password,passwordSalt);                            //null checker will do more stuff here.
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            Console.WriteLine(user);
        }

        public async Task<object?> updateuser(Guid uuid, User newUser)
        {
            var user = await _context.Users.FindAsync(uuid);
            if (!NullChecker.IsNull(user))
            {
                return null;
            }
            user.Nickname = newUser.Nickname; //this should go into the object factory too.
            user.Password = newUser.Password;
            user.Role = newUser.Role;
            user.Token = newUser.Token;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<object?> deleteUser(Guid pkey_uuid)
        {
            var user = await _context.Users.Where(d => d.Uuid.Equals(pkey_uuid)).FirstOrDefaultAsync();
            if(!NullChecker.IsNull(user))
            {
                _context.Users.Remove(user);    //isnt this blocking a thread?
                await _context.SaveChangesAsync();
                return user.Uuid;
            }
            return null;
        }

        //there has to be an authorization thing and stuff with jwt.
        //check how to use cancellation token.
        public async Task<string> loginUser(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Nickname == username);
            //check null
            var passwordHash = PasswordHashing.ComputeHash(password, user.Salt);
            if (user.Password == passwordHash)
            {
                var newUser = await _tokenUtil.createToken(user);
                await updateuser(user.Uuid, newUser);
                return newUser.Token;
            }
            else
            {
                return string.Empty;
            }

        }

        
    }
}
