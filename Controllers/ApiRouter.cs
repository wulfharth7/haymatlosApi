using haymatlosApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace haymatlosApi.Controllers
{
    [Route("api/users/v1")]
    [ApiController]
    //add responses
    public class ApiRouter : ControllerBase
    {
        readonly UserService _userService;
        public ApiRouter(UserService userService) => _userService = userService;
        
        
        // GET:  
        [HttpGet] 
        public async Task getUsers() 
        { 
            var users = await _userService.getUsers(); 
        }

        //POST: 
        [HttpPost("{nickname}/{password}")] 
        public async Task addUser(string nickname, string password) 
        { 
            await _userService.registerUser(nickname, password); 
        }

        //DELETE:
        [HttpDelete("{userId}")]
        public async Task deleteUser(Guid userId)
        {
            await _userService.deleteUser(userId);
        }

        [HttpGet("login/{nickname}/{password}")]
        public async Task loginUser(string nickname, string password)
        {
            await _userService.loginUser(nickname, password);
        }
    }
}
