using haymatlosApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace haymatlosApi.haymatlosApi.Core.Api
{

    [Route("/test/")]
    [ApiController]
    public class TestApi : ControllerBase
    {
        readonly UserService _userService;
        public TestApi(UserService userService) => _userService = userService;

        [Authorize(Roles = "user")]
        [HttpGet]
        public bool getUsers()
        {
            return true;
        }
    }

}

