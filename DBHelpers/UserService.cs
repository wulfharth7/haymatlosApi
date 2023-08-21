using haymatlosApi.Models;
using haymatlosApi.Utils;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

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
                Password = PasswordHashing.ComputeHash(password, passwordSalt)
            };
            await _context.Users.AddAsync(user);
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
        public async Task loginUser(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Nickname == username);
            //check null
            var passwordHash = PasswordHashing.ComputeHash(password, user.Salt);
            if (user.Password == passwordHash) Console.WriteLine("loggedin");
            else Console.WriteLine("wronggggg");

        }
    }
}
