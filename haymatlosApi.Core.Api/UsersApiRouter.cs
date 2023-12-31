﻿using haymatlosApi.haymatlosApi.Models;
using haymatlosApi.haymatlosApi.Utils.Objects;
using haymatlosApi.haymatlosApi.Utils.Pagination;
using haymatlosApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace haymatlosApi.Controllers
{
    [Route("/users/")]
    [AllowAnonymous]
    [ApiController]
    //add responses
    public class UsersApiRouter : ControllerBase
    {
        readonly UserService _userService;
        public UsersApiRouter(UserService userService) => _userService = userService;

        // GET:  
        [Authorize(Roles = "user")]
        [HttpGet] 
        public async Task<PaginatedResponse<IEnumerable<User>>> getUsers([FromQuery] PaginationFilter filter) 
        {
            var route = Request.Path.Value;
            return await _userService.getUsers(filter, route!); 
        }

        [HttpGet("id")] 
        [Authorize(Roles = "user")]
        public async Task<ResponseResult<User>> getUserById(Guid userId, bool getPosts = false)
        {
            return await _userService.getUserById(userId, getPosts);
        }

        //POST: 
        [AllowAnonymous]
        [HttpPost] 
        public async Task<bool> addUser(string nickname, string password) 
        { 
            return await _userService.registerUser(nickname, password); 
        }

        //UPDATE:
        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task updateUser(Guid userId, [FromBody] User user)
        {
            await _userService.updateuser(userId, user);
        }

        //DELETE:
        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task deleteUser(Guid userId)
        {
            await _userService.deleteUser(userId);
        }

        //LOGIN
        [AllowAnonymous]
        [HttpGet("login")]
        public async Task<User> loginUser(string nickname, string password)
        {
           return await _userService.loginUser(nickname, password);
          /* return await NullChecker.IsNullOrUndefinedAsync(token);*/
        }
    }
}
