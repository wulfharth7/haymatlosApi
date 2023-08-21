using haymatlosApi.haymatlosApi.Utils;
using haymatlosApi.Models;
using haymatlosApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace haymatlosApi.Controllers
{
    [Route("/users/")]  
    //[Authorize]
    [ApiController]
    //add responses
    public class ApiRouter : ControllerBase
    {
        readonly UserService _userService;
        public ApiRouter(UserService userService) => _userService = userService;


        // GET:  
        [Authorize(Roles = "admin")]
        [HttpGet] 
        public async Task getUsers() 
        { 
            var users = await _userService.getUsers(); 
        }

        //POST: 
        [Authorize(Roles = "admin")]
        [HttpPost("{nickname}/{password}")] 
        public async Task addUser(string nickname, string password) 
        { 
            await _userService.registerUser(nickname, password); 
        }

        //UPDATE:
        [Authorize(Roles = "admin")]
        [HttpPut("{userId}")]
        public async Task updateUser(Guid userId, [FromBody] User user)
        {
            await _userService.updateuser(userId, user);
        }

        //DELETE:
        [Authorize(Roles = "admin")]
        [HttpDelete("{userId}")]
        public async Task deleteUser(Guid userId)
        {
            await _userService.deleteUser(userId);
        }

        //LOGIN
        [AllowAnonymous]
        [HttpGet("login/{nickname}/{password}")]
        public async Task<HttpResponseMessage> loginUser(string nickname, string password)
        {
           var token = await _userService.loginUser(nickname, password);
           return await NullChecker.IsNullOrUndefinedAsync(token);
        }
    }
}
