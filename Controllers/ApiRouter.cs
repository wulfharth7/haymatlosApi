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
        public async Task<IActionResult> loginUser(string nickname, string password)
        {
           var token = await _userService.loginUser(nickname, password);
            if (token == null || token == String.Empty)
                return BadRequest(new { message = "User name or password is incorrect" });

            return Ok(token);
        }
    }
}
