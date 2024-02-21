using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.PowerBI.Api.Models;
using Savi.Core.IServices;
using Savi.Core.Services;

namespace Savi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserByIdAsync(string userId)
        {
            var response = await _userService.GetUserByIdAsync(userId);

            if (response.Succeeded)
            {
                return Ok(response.Data);
            }

            return StatusCode(response.StatusCode, new { errors = response.Errors });
        }
        [HttpGet("RegisteredUsersCount")]
        public ActionResult<int> GetRegisteredUsersCount()
        {
            try
            {
                var count = _userService.GetAllRegisteredUsers();

                if (count == 0)
                {                
                    return Ok("There are no new users registered today.");
                }
                return Ok($"The number of users registered today: {count}");
            }
            catch (Exception ex)
            {                
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


    }
}
