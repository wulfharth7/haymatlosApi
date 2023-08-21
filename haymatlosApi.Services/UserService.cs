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
        private readonly string passwordSalt = PasswordHashing.GenerateSalt();

        public UserService(PostgresContext postgresContext) => _context = postgresContext;

        public async Task<List<User>> getUsers()
        {
            var dataList = await _context.Users.AsNoTracking().ToListAsync();
            return dataList;
        }

        public async Task registerUser(string nickname, string password)
        {
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

        public async Task updateuser(Guid uuid, User newUser)
        {
            var user = await _context.Users.FindAsync(uuid);
            user.Nickname = newUser.Nickname;
            user.Password = newUser.Password;
            user.Role = newUser.Role;
            user.Token = newUser.Token;
            await _context.SaveChangesAsync();
        }

        public async Task deleteUser(Guid pkey_uuid)
        {
            var user = await _context.Users.Where(d => d.Uuid.Equals(pkey_uuid)).FirstOrDefaultAsync();
            if(user != null)
            {
                _context.Users.Remove(user); //isnt this blocking a thread?
                await _context.SaveChangesAsync();
            }
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
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("this_will_also_change_later");   //a config is needed for this.

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Nickname),
                    new Claim(ClaimTypes.Role, user.Role)
                }),

                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token =  tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);
                await updateuser(user.Uuid,user);
                return user.Token;

            }
            else
            {
                return string.Empty;
            }

        }
    }
}
