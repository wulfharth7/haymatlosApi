using haymatlosApi.haymatlosApi.Utils;
using haymatlosApi.Models;
using haymatlosApi.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace haymatlosApi.Services
{
    public class UserService
    {
        private PostgresContext _context;
        private readonly tokenUtil _tokenUtil;
        private readonly string passwordSalt = PasswordHashing.GenerateSalt();

        public UserService(PostgresContext postgresContext, tokenUtil tokenutil)
        {
            _context = postgresContext; 
            _tokenUtil = tokenutil;
        } 

        public async Task<List<User>> getUsers()
        {
            var dataList = await _context.Users.AsNoTracking().ToListAsync();
            return dataList;
        }

        public async Task registerUser(string nickname, string password)
        {
            //null checker will do more stuff here.
            var user = new User() { 
                Nickname = nickname,
                IsIndexed = false,
                Uuid = Guid.NewGuid(),
                Salt = passwordSalt,
                Role = "user",                                                              //there wont be much of admins so this is fine.
                Password = PasswordHashing.ComputeHash(password, passwordSalt)              //maybe for later there can be a service that decides what kind of role they'll have.
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<object?> updateuser(Guid uuid, User newUser)
        {
            var user = await _context.Users.FindAsync(uuid);
            if (!NullChecker.IsNull(user))
            {
                return null;
            }
            user.Nickname = newUser.Nickname;
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
